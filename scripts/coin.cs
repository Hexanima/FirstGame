using Godot;
using System;

public partial class coin : Area2D
{
	private game_manager gameManager;
	private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        gameManager = GetNode<game_manager>("%GameManager");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }
    private void _on_body_entered(Node2D body)
	{
		// Replace with function body.
		gameManager.AddPoint();
		animationPlayer.Play("pickup");
	}
}



