namespace Badengine;

public class BoxCollider : Component, ICollider {
    public bool OverlapsPoint(Vector2 point) {
        return point.X >= Transform.Position.X &&
               point.Y >= Transform.Position.Y &&
               point.X <= Transform.Position.X + Transform.Size.X &&
               point.Y <= Transform.Position.Y + Transform.Size.Y;
    }
}