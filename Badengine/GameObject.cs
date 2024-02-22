using Badengine.Engine;
using Badengine.Exceptions;

namespace Badengine;

public sealed class GameObject {
    public bool IsActive = true;

    private readonly List<Component> _components = new();
    private readonly List<Renderer> _renderers = new();
    internal Collider[] Colliders { get; private set; } = Array.Empty<Collider>();

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

        if (component is Renderer renderer) {
            _renderers.Add(renderer);
        }

        if (component is Collider collider) {
            Collider[] modifiedColliders = new Collider[Colliders.Length + 1];
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
            if (component.IsActive) {
                component.Update();
            }
        }
    }

    internal void FixedUpdate() {
        foreach (Component component in _components) {
            if (component.IsActive) {
                component.FixedUpdate();
            }
        }
    }

    internal void Start() {
        foreach (Component component in _components) {
            component.Start();
        }
    }

    internal void Render() {
        foreach (Renderer renderer in _renderers) {
            if (renderer.IsActive) {
                renderer.Render();
            }
        }
    }

    internal bool CheckColliderOverlaps(Vector2 point, Collider[] ignoredColliders) {
        return Colliders.Any(collider => collider.IsActive && !ignoredColliders.Contains(collider) && collider.OverlapsPoint(point));
    }

    internal bool CheckColliderOverlaps(Vector2 bottomLeft, Vector2 topRight, Collider[] ignoredColliders) {
        return Colliders.Any(collider => collider.IsActive && !ignoredColliders.Contains(collider) && collider.OverlapsBox(bottomLeft, topRight));
    }
}