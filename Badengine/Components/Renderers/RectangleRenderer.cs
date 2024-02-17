using Badengine.Engine;

namespace Badengine; 

public class RectangleRenderer : Renderer {
    public Color Color;
    
    public override void Render() {
        Graphics.DrawRectangle(GameObject.Transform.Position, GameObject.Transform.Size, Color);
    }
}