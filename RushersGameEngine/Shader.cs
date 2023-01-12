using Silk.NET.OpenGL;

namespace RushersGameEngine;

public class Shader : IDisposable {
    private readonly uint _handle;

    public Shader(string vertexShaderPath, string fragmentShaderPath) {
        var vertexShader = LoadShader(ShaderType.VertexShader, vertexShaderPath);
        var fragmentShader = LoadShader(ShaderType.VertexShader, fragmentShaderPath);

        _handle = Engine.Gl!.CreateProgram();
        
        Engine.Gl.AttachShader(_handle, vertexShader);
        Engine.Gl.AttachShader(_handle, fragmentShader);
        Engine.Gl.LinkProgram(_handle);
        
        Engine.Gl.GetProgram(_handle, GLEnum.LinkStatus, out var status);

        if (status == 0) {
            throw new Exception($"Failed to create texture: {Engine.Gl.GetProgramInfoLog(_handle)}");
        }
        
        Engine.Gl.DetachShader(_handle, vertexShader);
        Engine.Gl.DetachShader(_handle, fragmentShader);
        
        Engine.Gl.DeleteShader(vertexShader);
        Engine.Gl.DeleteShader(fragmentShader);
    }

    private static uint LoadShader(ShaderType type, string path) {
        var content = File.ReadAllText(path);
        var handle = Engine.Gl!.CreateShader(type);
        
        Engine.Gl.ShaderSource(handle, content);
        Engine.Gl.CompileShader(handle);

        var info = Engine.Gl.GetShaderInfoLog(handle);

        if (!string.IsNullOrEmpty(info)) {
            throw new Exception($"Failed to load {type}: {info}");
        }

        return handle;
    }

    public void Use() {
        Engine.Gl!.UseProgram(_handle);
    }

    public void SetUniform(string name, int value) {
        var location = Engine.Gl!.GetUniformLocation(_handle, name);

        if (location == -1) {
            throw new Exception($"{name} not found in shader");
        }
        
        Engine.Gl.Uniform1(location, value);
    }

    public void SetUniform(string name, float value) {
        var location = Engine.Gl!.GetUniformLocation(_handle, name);

        if (location == -1) {
            throw new Exception($"{name} not found in shader");
        }
        
        Engine.Gl.Uniform1(location, value);
    }

    public void Dispose() {
    }
}