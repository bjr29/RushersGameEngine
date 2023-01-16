using System.Numerics;

namespace RushersGameEngine.Nodes;

public class Node3D : Node {
    public Transform3D Transform { get; set; } = new();
}