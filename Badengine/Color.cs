namespace Badengine;

// Copied from Raylib_cs.Color
public struct Color {
    public byte R;
    public byte G;
    public byte B;
    public byte A;

    public Color(byte r, byte g, byte b, byte a) {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public Color(int r, int g, int b, int a) {
        R = Convert.ToByte(r);
        G = Convert.ToByte(g);
        B = Convert.ToByte(b);
        A = Convert.ToByte(a);
    }

    public Raylib_cs.Color ToRaylibColor() {
        return new Raylib_cs.Color(R, G, B, A);
    }

    public override string ToString() {
        return $"{{R:{R} G:{G} B:{B} A:{A}}}";
    }
}