using Raylib_cs;

namespace Badengine.Engine;

internal static class Graphics {
    private const float UnitsPerWidth = 20;
    private const float UnitsPerHeight = UnitsPerWidth / 16 * 9;

    private static float PixelsPerUnit => Raylib.GetScreenWidth() / UnitsPerWidth;

    internal static class UI {
        public enum TextOrigin {
            Left,
            Center,
            Right
        }

        public static void DrawText(string text, int x, int y, Color color, int fontSize, TextOrigin origin) {
            int width = Raylib.MeasureText(text, fontSize);
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

            Raylib.DrawText(text, xPos, y, fontSize, color.ToRaylibColor());
        }
    }

    internal static void BeginDrawing() {
        Raylib.BeginDrawing();
    }

    internal static void EndDrawing() {
        Raylib.EndDrawing();
    }

    private static int GetWrappedY(int scaledY, int scaledHeight) {
        return Raylib.GetScreenHeight() - (scaledY + scaledHeight);
    }

    public static void Clean() {
        Raylib.ClearBackground(Raylib_cs.Color.BLACK);
    }

    public static void DrawRectangle(Vector2 position, Vector2 size, Color color) {
        int scaledHeight = (int)(size.Y * PixelsPerUnit);

        Raylib.DrawRectangle(
            (int)(position.X * PixelsPerUnit),
            GetWrappedY((int)(position.Y * PixelsPerUnit), scaledHeight),
            (int)(size.X * PixelsPerUnit),
            scaledHeight,
            color.ToRaylibColor()
        );
    }
}