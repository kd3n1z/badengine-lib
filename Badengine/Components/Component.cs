using Badengine.Attributes;
using Badengine.Exceptions;

namespace Badengine;

public abstract class Component {
    private protected GameObject? _gameObject;

    public GameObject GameObject {
        get {
            if (_gameObject == null) {
                throw new ComponentNotRegisteredException();
            }

            return _gameObject;
        }
        set {
            if (Attribute.IsDefined(GetType(), typeof(NonRegistrable))) {
                throw new ComponentIsNonRegistrableException();
            }

            if (_gameObject != null) {
                throw new ComponentAlreadyRegisteredException();
            }

            _gameObject = value;
        }
    }

    public Transform Transform => _gameObject!.Transform;

    public T? GetComponent<T>() where T : Component => GameObject.GetComponent<T>();

    public virtual void Start() { }
    public virtual void Update() { }
}