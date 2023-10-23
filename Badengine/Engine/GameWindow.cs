using static Raylib_cs.Raylib;

namespace Badengine.Engine;

public class GameWindow {
    private Scene? _scene;

    public void LoadScene(Scene scene) {
        _scene = scene;
    }

    public void Run(string title) {
        InitWindow(800, 450, title);

        while (!WindowShouldClose()) {
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
        }

        CloseWindow();
    }
}