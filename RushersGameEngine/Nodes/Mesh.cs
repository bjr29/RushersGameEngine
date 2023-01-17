using Silk.NET.OpenGL;
using Shader = RushersGameEngine.Resources.Shader;
using Texture = RushersGameEngine.Resources.Texture;

namespace RushersGameEngine.Nodes;

public class Mesh : Node3D {
    public static Mesh Quad => new(
            new[] {
                0.5f,  0.5f, 0.0f, 1.0f, 0.0f,
                0.5f, -0.5f, 0.0f, 1.0f, 1.0f,
                -0.5f, -0.5f, 0.0f, 0.0f, 1.0f,
                -0.5f,  0.5f, 0.0f, 0.0f, 0.0f,
            },
            new uint[] {
                0, 1, 3,
                1, 2, 3,
            },
            new Shader("Shaders/Default.vert", "Shaders/Default.frag")
    );
    
    public float[] Vertices {
        get => _vertices;
        set {
            _vertices = value;
            GenerateMesh();
        }
    }

    public uint[] Indices {
        get => _indices;
        set {
            _indices = value; 
            GenerateMesh();
        }
    }

    public Shader Shader { get; set; }
    public Texture? Texture { get; set; } 
    
    private Buffer<float> _vertexBuffer = null!;
    private Buffer<uint> _indicesBuffer = null!;
    private VertexArray<float, uint> _vertexArray = null!;
    
    private float[] _vertices = null!;
    private uint[] _indices = null!;

    public Mesh(float[] vertices, uint[] indices, Shader shader, Texture? texture = null) {
        Vertices = vertices;
        Indices = indices;
        Shader = shader;
        Texture = texture;

        GenerateMesh();
        
        _vertexArray.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 5, 0);
        _vertexArray.VertexAttributePointer(1, 2, VertexAttribPointerType.Float, 5, 3);

        Render += GlRender;
    }

    private void GenerateMesh() {
        _vertexBuffer = new Buffer<float>(Vertices, BufferTargetARB.ArrayBuffer);
        _indicesBuffer = new Buffer<uint>(Indices, BufferTargetARB.ElementArrayBuffer);
        _vertexArray = new VertexArray<float, uint>(_vertexBuffer, _indicesBuffer);
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