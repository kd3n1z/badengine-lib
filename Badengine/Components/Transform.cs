using Badengine.Attributes;

namespace Badengine; 

[NonRegistrable]
public class Transform : Component {
    public Vector2 Position { get; set; } = Vector2.Zero;
    public Vector2 Size { get; set; } = Vector2.One;

    public Transform(GameObject gameObject) {
        _gameObject = gameObject;
    }
}