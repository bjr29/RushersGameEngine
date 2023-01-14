using Silk.NET.OpenGL;

namespace RushersGameEngine;

public class Mesh : Node3D, IDisposable {
    public float[] Vertices { get; set; }
    public uint[] Indices { get; set; }
    public Shader Shader { get; set; }

    private Buffer<float> _vertexBuffer;
    private Buffer<uint> _indicesBuffer;
    private VertexArray<float, uint> _vertexArray;

    public Mesh(float[] vertices, uint[] indices, Shader shader) {
        Vertices = vertices;
        Indices = indices;
        Shader = shader;

        _vertexBuffer = new Buffer<float>(Vertices, BufferTargetARB.ArrayBuffer);
        _indicesBuffer = new Buffer<uint>(Indices, BufferTargetARB.ElementArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _indicesBuffer);
        
        _vertexArray.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);

        Shader = shader;

        Render += GlRender;
    }

    private unsafe void GlRender(object? sender, EventArgs eventArgs) {
        _vertexArray.Bind();
        Shader.Use();
        
        Engine.Gl!.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
    }

    public override void Dispose() {
        _vertexBuffer.Dispose();
        _indicesBuffer.Dispose();
        _vertexArray.Dispose();
        Shader.Dispose();
        
        base.Dispose();
    }
}