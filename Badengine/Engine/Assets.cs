using SFML.Audio;
using SFML.Graphics;

namespace Badengine.Engine;

internal static class Assets {
    private static readonly string[] IgnoredFileNames = [".DS_Store"];

    private const string AssetsDirectoryName = "Assets";
    private const string FontsDirectoryName = "Fonts";
    private const string SoundsDirectoryName = "Sounds";
    private const string TexturesDirectoryName = "Textures";

    internal static readonly Dictionary<string, Font> Fonts = new Dictionary<string, Font>();
    internal static readonly Dictionary<string, SoundBuffer> SoundBuffers = new Dictionary<string, SoundBuffer>();
    internal static readonly Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();

    internal static void LoadAssets() {
        foreach (string fileName in ReadDirectory(FontsDirectoryName)) {
            Logger.LogInfo($"Loading font {fileName}...");
            Fonts.Add(Path.GetFileNameWithoutExtension(fileName), new Font(fileName));
        }

        foreach (string fileName in ReadDirectory(SoundsDirectoryName)) {
            Logger.LogInfo($"Loading sound {fileName}...");
            SoundBuffers.Add(Path.GetFileNameWithoutExtension(fileName), new SoundBuffer(fileName));
        }

        foreach (string fileName in ReadDirectory(TexturesDirectoryName)) {
            Logger.LogInfo($"Loading texture {fileName}...");
            Textures.Add(Path.GetFileNameWithoutExtension(fileName), new Texture(fileName));
        }

        Logger.LogInfo("Assets load finished");
    }

    private static IEnumerable<string> ReadDirectory(string directoryName) {
        string path = Path.Combine(AssetsDirectoryName, directoryName);

        if (!Directory.Exists(path)) {
            return Array.Empty<string>();
        }

        return Directory.GetFiles(path).Where(e => !IgnoredFileNames.Contains(Path.GetFileName(e)));
    }
}