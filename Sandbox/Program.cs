using System.Numerics;
using RushersGameEngine;
using RushersGameEngine.Input;
using RushersGameEngine.Nodes;
using RushersGameEngine.Resources;

Engine.Ready += Ready;

Engine.Start(title: "Sandbox");

void Ready(object? sender, EventArgs args) {
    var player = new Player();
    player.Transform.Position = new Vector2( 0, 0.5f );
    
    Engine.RootNode = player;
}

public class Player : Sprite {
    public Player() : base(new Texture("Images/RushersGameEngine.png")) {
        Update += OnUpdate;
    }

    private void OnUpdate(object? sender, EventArgs e) {
        if (InputManager.IsKeyDown("D")) {
            Transform.Position = Transform.Position with {
                X = Transform.Position.X + 0.001f,
            };
        }
    }
}
