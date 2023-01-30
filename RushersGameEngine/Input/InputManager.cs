using System.Numerics;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace RushersGameEngine.Input; 

public static class InputManager {
    private static IInputContext _input;

    public static void StartInput(IWindow window) {
        _input = window.CreateInput();

        foreach (var keyboard in _input.Keyboards) {
            AddKeyboard(keyboard);
        }
        
        foreach (var mouse in _input.Mice) {
            AddMouse(mouse);
        }
        
        foreach (var gamepad in _input.Gamepads) {
            AddGamepad(gamepad);
        }

        _input.ConnectionChanged += InputConnectionChanged;
    }

    // TODO: Replace string with enum 
    public static bool IsKeyDown(string key, int device = -1) {
        var parsedKey = (Key) Enum.Parse(typeof(Key), key);
        
        if (device < 0) {
            foreach (var keyboard in _input.Keyboards) {
                if (keyboard.IsKeyPressed(parsedKey)) {
                    return true;
                }
            }

        } else if (device < _input.Keyboards.Count) {
            return _input.Keyboards[device].IsKeyPressed(parsedKey);
        }

        return false;
    }

    // TODO: Replace string with enum
    public static bool IsMouseButtonDown(string button, int device = -1) {
        var parsedButton = (MouseButton) Enum.Parse(typeof(MouseButton), button);
        
        if (device < 0) {
            foreach (var mouse in _input.Mice) {
                if (mouse.IsButtonPressed(parsedButton)) {
                    return true;
                }
            }

        } else if (device < _input.Keyboards.Count) {
            return _input.Mice[device].IsButtonPressed(parsedButton);
        }

        return false;
    }

    private static void InputConnectionChanged(IInputDevice device, bool connected) {
        if (!connected) {
            return;
        }

        switch (device) {
            case IKeyboard keyboard:
                AddKeyboard(keyboard);
                break;

            case IMouse mouse:
                AddMouse(mouse);
                break;

            case IGamepad gamepad:
                AddGamepad(gamepad);
                break;
        }
    }

    private static void AddKeyboard(IKeyboard keyboard) {
        keyboard.KeyDown += KeyboardKeyDown;
        keyboard.KeyUp += KeyboardKeyUp;
        keyboard.KeyChar += KeyboardKeyTyped;
    }

    private static void AddMouse(IMouse mouse) {
        mouse.MouseDown += MouseButtonDown;
        mouse.MouseUp += MouseButtonUp;
        mouse.Click += MouseButtonClick;
        mouse.DoubleClick += MouseButtonDoubleClick;
        mouse.MouseMove += MouseMove;
    }

    private static void AddGamepad(IGamepad gamepad) {
        gamepad.ButtonDown += GamepadButtonDown;
        gamepad.ButtonUp += GamepadButtonUp;
        gamepad.ThumbstickMoved += GamepadThumbstickMoved;
        gamepad.TriggerMoved += GamepadTriggerMoved;
    }

    private static void GamepadTriggerMoved(IGamepad gamepad, Trigger trigger) {
        
    }

    private static void GamepadThumbstickMoved(IGamepad gamepad, Thumbstick thumbstick) {
        
    }

    private static void GamepadButtonUp(IGamepad gamepad, Button button) {
        
    }

    private static void GamepadButtonDown(IGamepad gamepad, Button button) {
        
    }

    private static void MouseMove(IMouse mouse, Vector2 position) {
        
    }

    private static void MouseButtonDoubleClick(IMouse mouse, MouseButton button, Vector2 position) {
        
    }

    private static void MouseButtonClick(IMouse mouse, MouseButton button, Vector2 position) {
        
    }

    private static void MouseButtonUp(IMouse mouse, MouseButton button) {
        
    }

    private static void MouseButtonDown(IMouse mouse, MouseButton button) {
        
    }

    private static void KeyboardKeyTyped(IKeyboard keyboard, char typed) {
        
    }

    private static void KeyboardKeyUp(IKeyboard keyboard, Key key, int scanCode) {
        
    }

    private static void KeyboardKeyDown(IKeyboard keyboard, Key key, int scanCode) {
        
    }
}