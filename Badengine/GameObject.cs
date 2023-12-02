using Badengine.Engine;
using Badengine.Exceptions;

namespace Badengine;

public sealed class GameObject {
    private readonly List<Component> _components = new();
    private readonly List<IRenderer> _renderers = new();
    private readonly List<ICollider> _colliders = new();
    public readonly Transform Transform;
    private Scene? _scene;

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

    public GameObject() {
        Transform = new Transform(this);
    }

    public void AddComponent(Component component) {
        component.GameObject = this;

        if (component is IRenderer renderer) {
            _renderers.Add(renderer);
        }

        if (component is ICollider collider) {
            _colliders.Add(collider);
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
    
    internal bool CheckColliderOverlaps(Vector2 point) {
        return _colliders.Any(collider => collider.OverlapsPoint(point));
    }
}