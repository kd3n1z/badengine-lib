using static Raylib_cs.Raylib;

namespace Badengine.Engine;

public static class Game {
    private static Scene? _scene;

    public static void LoadScene(Scene scene) {
        _scene = scene;
    }

    public static void Run(string title) {
        InitWindow(800, 450, title);

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
    }
}