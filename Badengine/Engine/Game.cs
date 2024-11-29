using Badengine.SceneManagement;
using SFML.Graphics;
using SFML.Window;

namespace Badengine.Engine;

public static class Game {
    public const string Version = "1.5.0";

    private static Scene? _scene;

    public static void SetScene(Scene scene) {
        _scene = scene;
    }

    public static void AddSceneBuilder(string sceneName, Func<Scene> builder) => SceneManager.AddSceneBuilder(sceneName, builder);

#if BADENGINE_DEBUG
    private const double DebugUpdateInterval = 0.5;
    private static int _debugPhysicsFrames;
#endif

    private static RenderWindow _window = null!;

    public static void Run(string title, float fixedDeltaTime = 0, string buildInfo = "") {
        _window = new RenderWindow(new VideoMode(800, 450),
#if BADENGINE_DEBUG
            "[DEBUG] " +
#endif
            title
        );

        _window.SetKeyRepeatEnabled(false);

        _window.Closed += (_, _) => { _window.Close(); };

        _window.KeyPressed += Input.OnKeyPressed;
        _window.KeyReleased += Input.OnKeyReleased;

        Graphics.SetRenderTarget(_window);

        Time.FixedDeltaTime = fixedDeltaTime;

        CancellationTokenSource physicsThreadCancellationTokenSource = new CancellationTokenSource();
        Thread physicsThread = new(() => PhysicsThread(fixedDeltaTime, physicsThreadCancellationTokenSource.Token));
        physicsThread.Start();


#if BADENGINE_DEBUG
        bool debugVisible = false;
        int debugAverageFps = 0;
        int debugAveragePhysicsFps = 0;
        DateTime debugPreviousFpsUpdate = DateTime.Now;
        int debugFrames = 0;

        string debugSfmlVersion = typeof(SFML.System.CSFML).Assembly.GetName().Version!.ToString();
#endif

        while (_window.IsOpen) {
            DateTime frameStart = DateTime.Now;

            Input.PreprocessEvents();
            _window.DispatchEvents();

            if (_scene == null) {
                Graphics.UI.DrawText("no scene set", 400, 225, new Color(255, 255, 255), 32,
                    Graphics.UI.TextOrigin.Center);
            }
            else {
                _scene.Start();
                _scene.Update();

                _scene.Render();
            }

#if BADENGINE_DEBUG
            debugFrames++;

            DateTime updateTime = DateTime.Now;
            double interval = updateTime.Subtract(debugPreviousFpsUpdate).TotalSeconds;

            if (interval > DebugUpdateInterval) {
                debugAverageFps = (int)(debugFrames / interval);
                debugAveragePhysicsFps = (int)(_debugPhysicsFrames / interval);
                debugPreviousFpsUpdate = updateTime;
                debugFrames = 0;
                _debugPhysicsFrames = 0;
            }

            if (Input.GetKeyDown(KeyCode.F1)) {
                debugVisible = !debugVisible;
            }


            if (debugVisible) {
                Graphics.UI.DrawText(
                    "fps: " + debugAverageFps +
                    "\npfps: " + debugAveragePhysicsFps +
                    "\ninfo:\n- SFML.Net v" + debugSfmlVersion +
                    "\n- badengine-lib v" + Version +
                    (buildInfo != "" ? "\n- " + buildInfo : ""),
                    20, 20, Color.Red, 18, Graphics.UI.TextOrigin.Left
                );
            }
#endif

            _window.Display();

            Time.DeltaTime = (float)(DateTime.Now - frameStart).TotalSeconds;
        }

        physicsThreadCancellationTokenSource.Cancel();
        physicsThread.Join();
    }

    private static void PhysicsThread(float deltaTime, CancellationToken cancellationToken) {
        DateTime previousFrameFinish = DateTime.Now;

        while (!cancellationToken.IsCancellationRequested) {
#if BADENGINE_DEBUG
            _debugPhysicsFrames++;
#endif

            DateTime frameStart = DateTime.Now;

            _scene?.FixedUpdate();

            double sleepTime = deltaTime - DateTime.Now.Subtract(previousFrameFinish).TotalSeconds;

            if (sleepTime > 0) {
                Thread.Sleep((int)(sleepTime * 1000));
            }

            previousFrameFinish = DateTime.Now;

            Time.FixedDeltaTime = (float)previousFrameFinish.Subtract(frameStart).TotalSeconds;
        }
    }
}