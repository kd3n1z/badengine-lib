using Badengine.SceneManagement;
using static Raylib_cs.Raylib;

namespace Badengine.Engine;

public static class Game {
    public const string Version = "1.1.0";

    private static Scene? _scene;

    public static void SetScene(Scene scene) {
        _scene = scene;
    }

    public static void AddSceneBuilder(string sceneName, Func<Scene> builder) => SceneManager.AddSceneBuilder(sceneName, builder);

    public static void Run(string title, float fixedDeltaTime = 0, bool debug = false, string buildInfo = "") {
        InitWindow(800, 450, title);

        Time.FixedDeltaTime = fixedDeltaTime;

        CancellationTokenSource physicsThreadCancellationTokenSource = new CancellationTokenSource();
        Thread physicsThread = new(() => PhysicsThread(physicsThreadCancellationTokenSource.Token, fixedDeltaTime));
        physicsThread.Start();

        bool debugVisible = false;
        float debugAverageDeltaTimeSum = 0;
        List<float> debugDeltaTimes = new();

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
                debugDeltaTimes.Add(Time.DeltaTime);
                debugAverageDeltaTimeSum += Time.DeltaTime;

                while (debugDeltaTimes.Count > 300) {
                    debugAverageDeltaTimeSum -= debugDeltaTimes[0];
                    debugDeltaTimes.RemoveAt(0);
                }
                
                if (Input.GetKeyDown(KeyCode.F1)) {
                    debugVisible = !debugVisible;
                }
                

                if (debugVisible) {
                    Graphics.UI.DrawText(
                        "avg fps: " + (int)(1 / (debugAverageDeltaTimeSum / debugDeltaTimes.Count)) + 
                        "\npfps: " + (int)(1 / Time.FixedDeltaTime) +
                        "\ninfo:\n- lib v" + Version + (buildInfo != "" ? "\n- " + buildInfo : ""),
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