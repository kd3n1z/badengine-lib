using Badengine.Engine;

namespace Badengine.SceneManagement;

public static class SceneManager {
    private static readonly Dictionary<string, Func<Scene>> SceneBuilders = new();

    internal static void AddSceneBuilder(string sceneName, Func<Scene> builder) {
        SceneBuilders.Add(sceneName, builder);
    }

    public static void LoadScene(string sceneName) {
        Game.SetScene(SceneBuilders[sceneName]());
    }
}