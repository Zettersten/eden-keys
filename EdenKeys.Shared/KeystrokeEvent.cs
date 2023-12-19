using EdenKeys.Shared.KeyboardListener;

namespace EdenKeys.Shared;

public record struct KeystrokeEvent(Keycode KeyCode, KeystrokeModifiers? ModifierFlag);