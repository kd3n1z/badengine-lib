using Badengine.Exceptions;

namespace Badengine;

public abstract class Component {
    private GameObject? _gameObject;

    public GameObject GameObject {
        get {
            if (_gameObject == null) {
                throw new ComponentNotRegisteredException();
            }

            return _gameObject;
        }
        set {
            if (_gameObject != null) {
                throw new ComponentAlreadyRegisteredException();
            }

            _gameObject = value;
        }
    }

    public T? GetComponent<T>() where T : Component => GameObject.GetComponent<T>();

    public virtual void Start() { }
    public virtual void Update() { }
}