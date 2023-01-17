using System.Numerics;
using RushersGameEngine;
using RushersGameEngine.Nodes;
using RushersGameEngine.Resources;

Engine.Ready += Ready;

Engine.Start(title: "Sandbox");

void Ready(object? sender, EventArgs args) {
    // var mesh = Mesh.Quad;
    // mesh.Texture = new Texture("Images/RushersGameEngine.png");
    // mesh.Transform.Position = new Vector3( 0.5f, 1, 0.5f);
    //
    // Engine.RootNode = mesh;

    var sprite = new Sprite(new Texture( "Images/RushersGameEngine.png" ));
    sprite.Transform.Position = new Vector2( 0.5f, 0.5f );
    Engine.RootNode = sprite;
}


