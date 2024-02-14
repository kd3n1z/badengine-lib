using Badengine.Engine;
using Badengine.Exceptions;

namespace Badengine;

public sealed class GameObject {
    private readonly List<Component> _components = new();
    private readonly List<IRenderer> _renderers = new();
    internal ICollider[] Colliders { get; private set; } = Array.Empty<ICollider>();

    public readonly Transform Transform;
    private Scene? _scene;

    public string Name = "New GameObject";
    public readonly string Id;

    internal Scene Scene {
        get {
            if (_scene == null) {
                throw new GameObjectNotRegisteredException();
            }

            return _scene;
        }
        set {
            if (_scene != null) {
                throw new GameObjectAlreadyRegisteredException();
            }

            _scene = value;
        }
    }

    public GameObject(string id) {
        Transform = new Transform(this);
        Id = id;
    }

    public GameObject() : this(Guid.NewGuid().ToString()) { }

    public void AddComponent(Component component) {
        component.GameObject = this;

        if (component is IRenderer renderer) {
            _renderers.Add(renderer);
        }

        if (component is ICollider collider) {
            ICollider[] modifiedColliders = new ICollider[Colliders.Length + 1];
            Array.Copy(Colliders, modifiedColliders, Colliders.Length);
            modifiedColliders[Colliders.Length] = collider;

            Colliders = modifiedColliders;
        }

        _components.Add(component);
    }

    public T? GetComponent<T>() where T : Component {
        foreach (Component component in _components) {
            if (component is T convertedComponent) {
                return convertedComponent;
            }
        }

        return null;
    }

    internal void Update() {
        foreach (Component component in _components) {
            component.Update();
        }
    }

    internal void FixedUpdate() {
        foreach (Component component in _components) {
            component.FixedUpdate();
        }
    }

    internal void Start() {
        foreach (Component component in _components) {
            component.Start();
        }
    }

    internal void Render() {
        foreach (IRenderer renderer in _renderers) {
            renderer.Render();
        }
    }

    internal bool CheckColliderOverlaps(Vector2 point, ICollider[] ignoredColliders) {
        return Colliders.Any(collider => !ignoredColliders.Contains(collider) && collider.OverlapsPoint(point));
    }

    internal bool CheckColliderOverlaps(Vector2 bottomLeft, Vector2 topRight, ICollider[] ignoredColliders) {
        return Colliders.Any(collider => !ignoredColliders.Contains(collider) && collider.OverlapsBox(bottomLeft, topRight));
    }
}