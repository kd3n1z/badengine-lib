namespace Badengine;

public abstract class Collider : Component {
    public abstract bool OverlapsPoint(Vector2 point);
    public abstract bool OverlapsBox(Vector2 bottomLeft, Vector2 topRight);
    public abstract Vector2[] GetPoints();
}