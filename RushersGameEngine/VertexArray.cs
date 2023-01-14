using Silk.NET.OpenGL;

namespace RushersGameEngine;

class VertexArray<TVertexType, TIndexType> : IDisposable
        where TVertexType : unmanaged
        where TIndexType : unmanaged {
    private readonly uint _handle;

    public VertexArray(Buffer<TVertexType> vertexBuffer, Buffer<TIndexType> indicesBuffer) {
        _handle = Engine.Gl!.GenVertexArray();

        Bind();
        vertexBuffer.Bind();
        indicesBuffer.Bind();
    }

    public unsafe void VertexAttributePointer(uint index, int count, VertexAttribPointerType type, uint vertexSize,
                                              int offset) {
        Engine.Gl!.VertexAttribPointer(
                index,
                count,
                type,
                false,
                vertexSize * (uint) sizeof(TVertexType),
                (void*) (offset * sizeof(TVertexType))
        );
        
        Engine.Gl!.EnableVertexAttribArray(index);
    }

    public void Bind() {
        Engine.Gl!.BindVertexArray(_handle);
    }

    public void Dispose() {
        Engine.Gl!.DeleteVertexArray(_handle);
    }
}