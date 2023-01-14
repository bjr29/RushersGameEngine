using RushersGameEngine.Nodes;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace RushersGameEngine;

public static class Engine {
    public static Node? RootNode { get; set; }
    
    internal static GL? Gl { get; private set; }

    private static IWindow? _window;

    public static event EventHandler? Ready;

    public static void Start(string title = "Game Window", Vector2D<int> windowSize = default, bool fullscreen = false,
                             bool maximised = false) {
        if (windowSize == default) {
            windowSize = new Vector2D<int>(800, 500);
        }
        
        var options = WindowOptions.Default;
        options.Size = windowSize;
        options.Title = title;

        var windowFlags = WindowState.Normal;

        if (fullscreen) {
            windowFlags = WindowState.Fullscreen;
            
        } else if (maximised) {
            windowFlags = WindowState.Maximized;
        }
        
        _window = Window.Create(options);
        _window.WindowState = windowFlags;
        
        _window.Load += Load;
        _window.Closing += Close;
        _window.Update += Update;
        _window.Render += Render;
        
        _window.Run();
    }

    private static void Load() {
        Gl = GL.GetApi(_window);
        
        var input = _window!.CreateInput();

        foreach (var keyboard in input.Keyboards) {
            keyboard.KeyDown += KeyboardKeyDown;
        }
        
        Ready?.Invoke(null, EventArgs.Empty);
    }

    private static void Close() {
        RootNode?.Dispose();
    }

    private static void KeyboardKeyDown(IKeyboard keyboard, Key key, int i) {
        Console.WriteLine(key.ToString());
    }

    private static void Update(double deltaTime) {
        RootNode?.InvokeUpdate(deltaTime);
    }

    private static void Render(double deltaTime) {
        Gl!.Clear((uint) ClearBufferMask.ColorBufferBit);
        
        RootNode?.InvokeRender(deltaTime);
    }
}