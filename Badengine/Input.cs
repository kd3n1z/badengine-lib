using SFML.Window;

namespace Badengine;

// Copied from SFML.Window.Keyboard.Scancode
public enum KeyCode {
    /// <summary>Represents any keycode not present in this enum</summary>
    Unknown = -1,
    A = 0,
    B,
    C,
    D,
    E,
    F,
    G,
    H,
    I,
    J,
    K,
    L,
    M,
    N,
    O,
    P,
    Q,
    R,
    S,
    T,
    U,
    V,
    W,
    X,
    Y,
    Z,
    Num1,
    Num2,
    Num3,
    Num4,
    Num5,
    Num6,
    Num7,
    Num8,
    Num9,
    Num0,
    Enter,
    Escape,
    Backspace,
    Tab,
    Space,
    Hyphen,
    Equal,
    LBracket,
    RBracket,
    Backslash,
    Semicolon,
    Apostrophe,
    Grave,
    Comma,
    Period,
    Slash,
    F1,
    F2,
    F3,
    F4,
    F5,
    F6,
    F7,
    F8,
    F9,
    F10,
    F11,
    F12,
    F13,
    F14,
    F15,
    F16,
    F17,
    F18,
    F19,
    F20,
    F21,
    F22,
    F23,
    F24,
    CapsLock,
    PrintScreen,
    ScrollLock,
    Pause,
    Insert,
    Home,
    PageUp,
    Delete,
    End,
    PageDown,
    Right,
    Left,
    Down,
    Up,
    NumLock,
    NumpadDivide,
    NumpadMultiply,
    NumpadMinus,
    NumpadPlus,
    NumpadEqual,
    NumpadEnter,
    NumpadDecimal,
    Numpad1,
    Numpad2,
    Numpad3,
    Numpad4,
    Numpad5,
    Numpad6,
    Numpad7,
    Numpad8,
    Numpad9,
    Numpad0,
    NonUsBackslash,
    Application,
    Execute,
    ModeChange,
    Help,
    Menu,
    Select,
    Redo,
    Undo,
    Cut,
    Copy,
    Paste,
    VolumeMute,
    VolumeUp,
    VolumeDown,
    MediaPlayPause,
    MediaStop,
    MediaNextTrack,
    MediaPreviousTrack,
    LeftControl,
    LeftShift,
    LeftAlt,
    LeftSystem,
    RightControl,
    RightShift,
    RightAlt,
    RightSystem,
    Back,
    Forward,
    Refresh,
    Stop,
    Search,
    Favorites,
    HomePage,
    LaunchApplication1,
    LaunchApplication2,
    LaunchMail,
    LaunchMediaSelect,
}

[Flags]
internal enum KeyState : byte {
    Released = 0,
    Pressed = 1 << 0,
    PressedInCurrentFrame = 1 << 1,
    ReleasedInCurrentFrame = 1 << 2
}

public static class Input {
    private static readonly KeyState[] KeyStates = new KeyState[(int)Keyboard.Scancode.ScancodeCount];

    internal static void PreprocessEvents() {
        for (int i = 0; i < KeyStates.Length; i++) {
            KeyStates[i] &= KeyState.Pressed;
        }
    }


    internal static void OnKeyPressed(object? _, KeyEventArgs e) {
        int scancode = (int)e.Scancode;

        if (!GetKeyState((KeyCode)scancode, KeyState.Pressed)) {
            KeyStates[scancode] = KeyState.Pressed | KeyState.PressedInCurrentFrame;
        }
    }

    internal static void OnKeyReleased(object? _, KeyEventArgs e) {
        int scancode = (int)e.Scancode;

        if (GetKeyState((KeyCode)scancode, KeyState.Pressed)) {
            KeyStates[scancode] = KeyState.Released | KeyState.ReleasedInCurrentFrame;
        }
    }

    internal static bool GetKeyState(KeyCode key, KeyState state) => (KeyStates[(int)key] & state) == state;

    public static bool GetKeyDown(KeyCode key) => GetKeyState(key, KeyState.PressedInCurrentFrame);

    public static bool GetKeyUp(KeyCode key) => GetKeyState(key, KeyState.ReleasedInCurrentFrame);

    public static bool GetKey(KeyCode key) => GetKeyState(key, KeyState.Pressed);
}