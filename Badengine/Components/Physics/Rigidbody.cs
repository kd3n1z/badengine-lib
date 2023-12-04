namespace Badengine;

public class Rigidbody : Component {
    public float Gravity = -9.81f;
    public Vector2 Velocity = new();

    public override void Update() {
        Velocity.Y += Gravity * Time.DeltaTime;
        ICollider[] colliders = GameObject.Colliders;

        Vector2 move = Velocity * Time.DeltaTime;

        if (!CheckOverlapsWithOffset(new Vector2(move.X, 0), colliders)) {
            Transform.Position.X += move.X;
        }
        else {
            move.X = 0;
        }

        if (!CheckOverlapsWithOffset(new Vector2(0, move.Y), colliders)) {
            Transform.Position.Y += move.Y;
        }
        else {
            Velocity.Y = 0;
        }
    }

    public bool CheckOverlapsWithOffset(Vector2 offset, ICollider[] colliders) {
        return colliders.Select(collider => collider.GetPoints())
            .Any(points =>
                GameObject.Scene.CheckColliderOverlaps(points[0] + offset, points[1] + offset, colliders)
            );
    }
}