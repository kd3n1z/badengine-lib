namespace Badengine;

public class BoxCollider : Collider {
    public override bool OverlapsPoint(Vector2 point) {
        Vector2[] points = GetPoints();

        return point.X > points[0].X &&
               point.Y > points[0].Y &&
               point.X < points[1].X &&
               point.Y < points[1].Y;
    }

    public override bool OverlapsBox(Vector2 bottomLeft, Vector2 topRight) {
        Vector2[] points = GetPoints();

        return topRight.X > points[0].X &&
               topRight.Y > points[0].Y &&
               bottomLeft.X < points[1].X &&
               bottomLeft.Y < points[1].Y;
    }

    public override Vector2[] GetPoints() {
        // returns [bottomLeft, topRight]

        return new[] { Transform.Position, Transform.Position + Transform.Size };
    }
}