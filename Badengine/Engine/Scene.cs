namespace Badengine.Engine;

public sealed class Scene {
    private readonly List<GameObject> _gameObjects = new List<GameObject>();

    public void AddGameObject(GameObject gameObject) {
        gameObject.Scene = this;

        _gameObjects.Add(gameObject);
    }

    private bool _startCalled;

    internal void Start() {
        if (!_startCalled) {
            _startCalled = true;
            foreach (GameObject gameObject in _gameObjects) {
                gameObject.Start();
            }
        }
    }

    internal void Update() {
        foreach (GameObject gameObject in _gameObjects) {
            if (gameObject.IsActive) {
                gameObject.Update();
            }
        }
    }

    internal void FixedUpdate() {
        foreach (GameObject gameObject in _gameObjects) {
            if (gameObject.IsActive) {
                gameObject.FixedUpdate();
            }
        }
    }

    internal void Render() {
        Graphics.Clean();

        foreach (GameObject gameObject in _gameObjects) {
            if (gameObject.IsActive) {
                gameObject.Render();
            }
        }
    }

    internal bool CheckColliderOverlaps(Vector2 point, ICollider[] ignoredColliders) {
        return _gameObjects.Any(gameObject => gameObject.CheckColliderOverlaps(point, ignoredColliders));
    }

    internal bool CheckColliderOverlaps(Vector2 bottomLeft, Vector2 topRight, ICollider[] ignoredColliders) {
        return _gameObjects.Any(gameObject => gameObject.CheckColliderOverlaps(bottomLeft, topRight, ignoredColliders));
    }
}