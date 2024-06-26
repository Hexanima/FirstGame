using Godot;
using System;

public partial class coin : Area2D
{
	private game_manager gameManager;

    public override void _Ready()
    {
        gameManager = GetNode<game_manager>("%GameManager");
    }
    private void _on_body_entered(Node2D body)
	{
		// Replace with function body.
		gameManager.AddPoint();
		this.QueueFree();
	}
}



