using Silk.NET.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RushersGameEngine.Resources; 

public class Texture : Resource {
    private readonly uint _handle;

    public unsafe Texture(string path) {
        _handle = Engine.Gl.GenTexture();
        Bind();

        using var image = Image.Load<Rgba32>(path);
        
        Engine.Gl.TexImage2D(
                TextureTarget.Texture2D,
                0,
                InternalFormat.Rgba8,
                (uint) image.Width,
                (uint) image.Height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                null
        );
        
        image.ProcessPixelRows(
                accessor => {
                    for (var y = 0; y < accessor.Height; y++) {
                        fixed (void* data = accessor.GetRowSpan(y)) {
                            Engine.Gl.TexSubImage2D(
                                    TextureTarget.Texture2D,
                                    0,
                                    0,
                                    y,
                                    (uint) accessor.Width,
                                    1,
                                    PixelFormat.Rgba,
                                    PixelType.UnsignedByte,
                                    data
                            );
                        }
                    }
                }
        );

        SetParameters();
    }

    public unsafe Texture(Span<byte> data, uint width, uint height) {
        _handle = Engine.Gl.GenTexture();
        Bind();

        fixed (void* dat = &data[0]) {
            Engine.Gl.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    (int) InternalFormat.Rgba,
                    width,
                    height,
                    0,
                    PixelFormat.Rgba,
                    PixelType.UnsignedByte,
                    dat
            );

            SetParameters();
        }
    }

    internal void Bind(TextureUnit index = TextureUnit.Texture0) {
        Engine.Gl.ActiveTexture(index);
        Engine.Gl.BindTexture(TextureTarget.Texture2D, _handle);
    }

    private void SetParameters() {
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureWrapS, (int) GLEnum.ClampToEdge);
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureWrapT, (int) GLEnum.ClampToEdge);
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureMinFilter, (int) GLEnum.LinearMipmapLinear);
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureMagFilter, (int) GLEnum.Linear);
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureBaseLevel, 0);
        Engine.Gl.TexParameter(TextureTarget.Texture2D, GLEnum.TextureMaxLevel, 8);
        
        Engine.Gl.GenerateMipmap(TextureTarget.Texture2D);
    }

    public override void Dispose() {
        Engine.Gl.DeleteTexture(_handle);
    }
}