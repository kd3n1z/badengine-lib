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
            BeginDrawing();
            
            Graphics.Clean();

            if (_scene == null) {
                Graphics.RenderText("no scene set", 400, 225, new Color(255, 255, 255, 255), 32, TextOrigin.Center);
            }
            else {
                _scene.Render();
            }

            EndDrawing();
        }
        
        CloseWindow();
    }
}