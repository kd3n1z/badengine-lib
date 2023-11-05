namespace Badengine;

public static class Random {
    private static readonly System.Random _random = new();

    public static float Range(float min, float max) {
        return (float)(min + _random.NextDouble() * (max - min));
    }

    public static int Range(int min, int max) {
        return _random.Next(min, max);
    }
}