using Badengine.Engine;

namespace Badengine;

public sealed class GameObject {
    private readonly List<Component> _components = new List<Component>();
    private readonly List<IRenderer> _renderers = new List<IRenderer>();
    private Scene? _scene;

    internal Scene Scene {
        get {
            if (_scene == null) {
                throw new Exception("gameObject not registered");
            }

            return _scene;
        }
        set {
            if (_scene != null) {
                throw new Exception("gameObject already registered");
            }

            _scene = value;
        }
    }

    public void AddComponent(Component component) {
        component.GameObject = this;

        if (component is IRenderer renderer) {
            _renderers.Add(renderer);
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
}