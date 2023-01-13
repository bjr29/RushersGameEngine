using Silk.NET.OpenGL;

namespace RushersGameEngine;

public class Mesh {
    public float[] Vertices { get; set; }
    public uint[] Faces { get; set; }
    
    private Buffer<float> _vertexBuffer;
    private Buffer<uint> _edgeBuffer;
    private VertexArray<float, uint> _vertexArray;

    public Mesh(float[] vertices, uint[] faces) {
        Vertices = vertices;
        Faces = faces;
        
        _vertexBuffer = new Buffer<float>(Vertices, BufferTargetARB.ArrayBuffer);
        _edgeBuffer = new Buffer<uint>(Faces, BufferTargetARB.ArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _edgeBuffer);
        
        // todo
    }
}