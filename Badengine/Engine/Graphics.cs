using static Raylib_cs.Raylib;

namespace Badengine.Engine;

internal static class Graphics {
    internal static class UI {
        public enum TextOrigin {
            Left,
            Center,
            Right
        }

        public static void RenderText(string text, int x, int y, Color color, int fontSize, TextOrigin origin) {
            int width = MeasureText(text, fontSize);
            int xPos = x;
            switch (origin) {
                case TextOrigin.Left:
                    break;
                case TextOrigin.Center:
                    xPos -= width / 2;
                    break;
                case TextOrigin.Right:
                    xPos -= width;
                    break;
            }

            DrawText(text, xPos, y, fontSize, color.ToRaylibColor());
        }
    }

    public static void Clean() {
        ClearBackground(Raylib_cs.Color.BLACK);
    }
}