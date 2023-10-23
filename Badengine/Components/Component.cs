namespace Badengine;

public abstract class Component {
    private GameObject? _gameObject;

    public GameObject GameObject {
        get {
            if (_gameObject == null) {
                throw new Exception("component must be registered");
            }

            return _gameObject;
        }
        set {
            if (_gameObject != null) {
                throw new Exception("component already registered");
            }

            _gameObject = value;
        }
    }

    public virtual void Start() { }
    public virtual void Update() { }
}