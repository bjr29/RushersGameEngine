using RushersGameEngine;

Engine.Ready += Ready;

Engine.Start(title: "Sandbox");

void Ready(object? sender, EventArgs args) {
    Engine.RootNode = new Mesh(
        new[] {
            0.5f, 0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f, 0.5f, 0.5f,
        },
        new uint[] {
            0, 1, 3,
            1, 2, 3,
        },
        new Shader("Shaders/Shader.vert", "Shaders/Shader.frag")
    );
}


