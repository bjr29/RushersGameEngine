using Silk.NET.OpenGL;
using Shader = RushersGameEngine.Resources.Shader;
using Texture = RushersGameEngine.Resources.Texture;

namespace RushersGameEngine.Nodes; 

public class Sprite : Node2D {
    private readonly float[] _vertices = {
         0.5f,  0.5f, 0, 1.0f, 0.0f,
         0.5f, -0.5f, 0, 1.0f, 1.0f,
        -0.5f, -0.5f, 0, 0.0f, 1.0f,
        -0.5f,  0.5f, 0, 0.0f, 0.0f,
    };

    private readonly uint[] _indices = {
        0, 1, 3,
        1, 2, 3,
    };

    public Shader Shader { get; set; } = new("Shaders/Default.vert", "Shaders/Default.frag");
    public Texture? Texture { get; set; } 
    
    private readonly Buffer<float> _vertexBuffer;
    private readonly Buffer<uint> _indicesBuffer;
    private readonly VertexArray<float, uint> _vertexArray;
    
    public Sprite(Texture texture, Shader? shader = null) {
        if (shader != null) {
            Shader = shader;
        }

        Texture = texture;

        _vertexBuffer = new Buffer<float>(_vertices, BufferTargetARB.ArrayBuffer);
        _indicesBuffer = new Buffer<uint>(_indices, BufferTargetARB.ElementArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _indicesBuffer);
        
        _vertexArray.VertexAttributePointer(0, 2, VertexAttribPointerType.Float, 5, 0);
        _vertexArray.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        Render += GlRender;
    }

    private unsafe void GlRender(object? sender, EventArgs eventArgs) {
        _vertexArray.Bind();
        Shader.Use();

        if (Texture != null) {
            Texture.Bind();
            Shader.SetUniform("uTexture0", 0);
        }
        
        Shader.SetUniform("uModel", Transform.ViewMatrix);
        Engine.Gl.DrawElements(PrimitiveType.Triangles, (uint) _indices.Length, DrawElementsType.UnsignedInt, null);
    }

    public override void Dispose() {
        _vertexBuffer.Dispose();
        _indicesBuffer.Dispose();
        _vertexArray.Dispose();
        Shader.Dispose();
        
        base.Dispose();
    }
}