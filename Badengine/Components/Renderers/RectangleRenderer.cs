using Badengine.Engine;

namespace Badengine; 

public class RectangleRenderer : Component, IRenderer {
    public Color Color;
    
    public void Render() {
        Graphics.DrawRectangle(GameObject.Transform.Position, GameObject.Transform.Size, Color);
    }
}