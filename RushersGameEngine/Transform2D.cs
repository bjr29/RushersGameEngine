using System.Numerics;

namespace RushersGameEngine; 

public class Transform2D {
    public Vector2 Position { get; set; } = new();
    public Vector2 Scale { get; set; } = new(1);
    public float Rotation { get; set; } = 0;

    internal Matrix4x4 ViewMatrix => Matrix4x4.Identity
                                     * Matrix4x4.CreateFromQuaternion(Quaternion.CreateFromAxisAngle(Vector3.UnitZ, Rotation))
                                     * Matrix4x4.CreateScale(new Vector3(Scale.X, Scale.Y, 0))
                                     * Matrix4x4.CreateTranslation(new Vector3(Position.X, Position.Y, 0));
}