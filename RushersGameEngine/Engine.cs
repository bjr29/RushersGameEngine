using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace RushersGameEngine;

public static class Engine {
    internal static GL? Gl { private set; get; }
    
    private static IWindow? _window;

    private static Buffer<float>? _vertexBuffer;
    private static Buffer<uint>? _edgeBuffer;
    private static VertexArray<float, uint>? _vertexArray;
    
    public static void Start(string title = "Game Window", Vector2D<int> windowSize = default/*, bool fullscreen = false,
                             bool maximised = false*/) {
        if (windowSize == default) {
            windowSize = new Vector2D<int>(800, 500);
        }
        
        var options = WindowOptions.Default;
        options.Size = windowSize;
        options.Title = title;
        
        _window = Window.Create(options);
        
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

        
    }

    private static void Close() {
        
    }

    private static void KeyboardKeyDown(IKeyboard keyboard, Key key, int i) {
        Console.WriteLine(key.ToString());
    }

    private static void Update(double obj) {
        
    }

    private static void Render(double obj) {
        
    }
}