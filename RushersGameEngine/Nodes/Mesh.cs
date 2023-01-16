using Silk.NET.OpenGL;
using Shader = RushersGameEngine.Resources.Shader;
using Texture = RushersGameEngine.Resources.Texture;

namespace RushersGameEngine.Nodes;

public class Mesh : Node3D {
    public float[] Vertices { get; set; }
    public uint[] Indices { get; set; }
    public Shader Shader { get; set; }
    public Texture? Texture { get; set; } 
    
    private readonly Buffer<float> _vertexBuffer;
    private readonly Buffer<uint> _indicesBuffer;
    private readonly VertexArray<float, uint> _vertexArray;

    public Mesh(float[] vertices, uint[] indices, Shader shader, Texture texture) {
        Vertices = vertices;
        Indices = indices;
        Shader = shader;
        Texture = texture;

        _vertexBuffer = new Buffer<float>(Vertices, BufferTargetARB.ArrayBuffer);
        _indicesBuffer = new Buffer<uint>(Indices, BufferTargetARB.ElementArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _indicesBuffer);
        
        _vertexArray.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
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
        Engine.Gl.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
    }

    public override void Dispose() {
        _vertexBuffer.Dispose();
        _indicesBuffer.Dispose();
        _vertexArray.Dispose();
        Shader.Dispose();
        
        base.Dispose();
    }
}