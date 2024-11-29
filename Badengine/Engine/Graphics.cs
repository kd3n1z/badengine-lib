using SFML.Graphics;
using SFML.System;

namespace Badengine.Engine;

internal static class Graphics {
    private const float UnitsPerWidth = 20;
    private const float UnitsPerHeight = UnitsPerWidth / 16 * 9;

    private static float PixelsPerUnit => _renderTarget.Size.X / UnitsPerWidth;

    private static RenderTarget _renderTarget;

    public static void SetRenderTarget(RenderTarget target) {
        _renderTarget = target;
    }

    internal static class UI {
        public enum TextOrigin {
            Left,
            Center,
            Right
        }

        public static void DrawText(string text, int x, int y, Color color, int fontSize, TextOrigin origin) {
            // TODO
            
            // int width = Raylib.MeasureText(text, fontSize);
            // int xPos = x;
            // switch (origin) {
            //     case TextOrigin.Left:
            //         break;
            //     case TextOrigin.Center:
            //         xPos -= width / 2;
            //         break;
            //     case TextOrigin.Right:
            //         xPos -= width;
            //         break;
            // }
            //
            // Raylib.DrawText(text, xPos, y, fontSize, color.ToRaylibColor());
        }
    }

    private static float GetWrappedY(float scaledY, float scaledHeight) {
        return _renderTarget.Size.Y - (scaledY + scaledHeight);
    }

    public static void Clear() {
        _renderTarget.Clear(SFML.Graphics.Color.Black);
    }

    public static void DrawRectangle(Vector2 position, Vector2 size, Color color) {
        float scaledHeight = size.Y * PixelsPerUnit;

        _renderTarget.Draw(new RectangleShape(new Vector2f(size.X * PixelsPerUnit, scaledHeight)) {
            FillColor = color,
            Position = new Vector2f(position.X * PixelsPerUnit, GetWrappedY(position.Y * PixelsPerUnit, scaledHeight))
        });
    }
}