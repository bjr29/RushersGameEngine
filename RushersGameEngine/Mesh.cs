using Silk.NET.OpenGL;

namespace RushersGameEngine;

public class Mesh {
    public float[] Vertices { get; set; }
    public uint[] Faces { get; set; }
    public Shader Shader { get; set; }

    private Buffer<float> _vertexBuffer;
    private Buffer<uint> _edgeBuffer;
    private VertexArray<float, uint> _vertexArray;

    public Mesh(float[] vertices, uint[] faces, Shader shader) {
        Vertices = vertices;
        Faces = faces;
        Shader = shader;

        _vertexBuffer = new Buffer<float>(Vertices, BufferTargetARB.ArrayBuffer);
        _edgeBuffer = new Buffer<uint>(Faces, BufferTargetARB.ArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _edgeBuffer);
        
        _vertexArray.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 3, 0);

        Shader = shader;
    }
}