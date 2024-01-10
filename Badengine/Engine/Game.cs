using Badengine.SceneManagement;
using static Raylib_cs.Raylib;

namespace Badengine.Engine;

public static class Game {
    public const string Version = "1.0.0";

    private static Scene? _scene;

    public static void SetScene(Scene scene) {
        _scene = scene;
    }

    public static void AddSceneBuilder(string sceneName, Func<Scene> builder) => SceneManager.AddSceneBuilder(sceneName, builder);

    public static void Run(string title, float fixedDeltaTime = 0, bool debug = false) {
        InitWindow(800, 450, title);

        Time.FixedDeltaTime = fixedDeltaTime;

        CancellationTokenSource physicsThreadCancellationTokenSource = new CancellationTokenSource();
        Thread physicsThread = new(() => PhysicsThread(physicsThreadCancellationTokenSource.Token, fixedDeltaTime));
        physicsThread.Start();

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

            Graphics.EndDrawing();

            Time.DeltaTime = (float)(DateTime.Now - frameStart).TotalSeconds;
        }

        CloseWindow();

        physicsThreadCancellationTokenSource.Cancel();
        physicsThread.Join();
    }

    private static void PhysicsThread(CancellationToken cancellationToken, float deltaTime) {
        DateTime previousFrameFinish = DateTime.Now;

        while (!cancellationToken.IsCancellationRequested) {
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