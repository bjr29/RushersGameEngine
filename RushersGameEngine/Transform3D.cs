using System.Numerics;

namespace RushersGameEngine; 

public class Transform3D {
    public Vector3 Position { get; set; } = new();
    public Vector3 Scale { get; set; } = new(1);
    public Quaternion Rotation { get; set; } = Quaternion.Identity;

    internal Matrix4x4 ViewMatrix => Matrix4x4.Identity
                                     * Matrix4x4.CreateFromQuaternion(Rotation)
                                     * Matrix4x4.CreateScale(Scale)
                                     * Matrix4x4.CreateTranslation(Position);
}