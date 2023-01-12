using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace RushersGameEngine;

public static class Engine {
    private static IWindow? window;

    public static void Start(Vector2D<int> windowSize, string title = "Game Window", bool fullscreen = false) {
        var options = WindowOptions.Default;
        options.Size = windowSize;
        options.Title = title;

        window = Window.Create(options);
        
        window.Load += WindowOnLoad;
        window.Update += WindowOnUpdate;
        window.Render += WindowOnRender;
        
        window.Run();
    }

    private static void WindowOnRender(double obj) {
        
    }

    private static void WindowOnUpdate(double obj) {
        
    }

    private static void WindowOnLoad() {
        
    }
}