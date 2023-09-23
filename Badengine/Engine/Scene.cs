namespace Badengine.Engine; 

public sealed class Scene {
    private readonly List<GameObject> _gameObjects = new List<GameObject>();

    public void AddGameObject(GameObject gameObject) {
        gameObject.Scene = this;
        
        _gameObjects.Add(gameObject);
    }
    
    internal void Start() {
        foreach (GameObject go in _gameObjects) {
            go.Start();
        }
    }
        
    internal void Update() {
        foreach (GameObject go in _gameObjects) {
            go.Update();
        }
    }

    internal void Render() {
        
    }
}