using Silk.NET.OpenGL;

namespace RushersGameEngine;

class Buffer<T> : IDisposable where T : unmanaged {
    private readonly uint _handle;
    private readonly BufferTargetARB _bufferType;

    internal unsafe Buffer(Span<T> data, BufferTargetARB bufferType) {
        _bufferType = bufferType;

        _handle = Engine.Gl!.GenBuffer();
        Bind();

        fixed (void* dat = data) {
            Engine.Gl.BufferData(bufferType, (nuint) (data.Length * sizeof(T)), dat, GLEnum.StaticDraw);
        }
    }

    internal void Bind() {
        Engine.Gl!.BindBuffer(_bufferType, _handle);
    }

    public void Dispose() {
        Engine.Gl!.DeleteBuffer(_handle);
    }
}