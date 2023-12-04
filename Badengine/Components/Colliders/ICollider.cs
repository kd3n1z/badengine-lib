namespace Badengine;

public interface ICollider {
    public bool OverlapsPoint(Vector2 point);
    public bool OverlapsBox(Vector2 bottomLeft, Vector2 topRight);
    public Vector2[] GetPoints();
}