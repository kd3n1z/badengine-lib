using Badengine.SceneManagement;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace Badengine.Engine;

public static class Game {
    public const string Version = "1.4.2";

    private static Scene? _scene;

    public static void SetScene(Scene scene) {
        _scene = scene;
    }

    public static void AddSceneBuilder(string sceneName, Func<Scene> builder) => SceneManager.AddSceneBuilder(sceneName, builder);

    private static int _debugPhysicsFrames;

    public static void Run(string title, float fixedDeltaTime = 0, bool debug = false, string buildInfo = "") {
        InitWindow(800, 450, title);

        SetExitKey(0);

        Time.FixedDeltaTime = fixedDeltaTime;

        CancellationTokenSource physicsThreadCancellationTokenSource = new CancellationTokenSource();
        Thread physicsThread = new(() => PhysicsThread(physicsThreadCancellationTokenSource.Token, fixedDeltaTime, debug));
        physicsThread.Start();

        bool debugVisible = false;
        int debugAverageFps = 0;
        int debugAveragePhysicsFps = 0;
        DateTime debugPreviousFpsUpdate = DateTime.Now;
        int debugFrames = 0;

        while (!WindowShouldClose()) {
            DateTime frameStart = DateTime.Now;

            Graphics.BeginDrawing();

            if (_scene == null) {
                Graphics.UI.DrawText("no scene set", 400, 225, new Color(255, 255, 255), 32,
                    Graphics.UI.TextOrigin.Center);
            }
            else {
                _scene.Start();
                _scene.Update();

                _scene.Render();
            }

            if (debug) {
                debugFrames++;

                DateTime updateTime = DateTime.Now;
                double interval = updateTime.Subtract(debugPreviousFpsUpdate).TotalSeconds;

                if (interval > 0.5) {
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
                        "\ninfo:\n- raylib v" + RAYLIB_VERSION +
                        "\n- badengine-lib v" + Version +
                        (buildInfo != "" ? "\n- " + buildInfo : ""),
                        20, 20, Color.Red, 18, Graphics.UI.TextOrigin.Left
                    );
                }
            }

            Graphics.EndDrawing();

            Time.DeltaTime = (float)(DateTime.Now - frameStart).TotalSeconds;
        }

        CloseWindow();

        physicsThreadCancellationTokenSource.Cancel();
        physicsThread.Join();
    }

    private static void PhysicsThread(CancellationToken cancellationToken, float deltaTime, bool debug) {
        DateTime previousFrameFinish = DateTime.Now;

        while (!cancellationToken.IsCancellationRequested) {
            if (debug) {
                _debugPhysicsFrames++;
            }

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