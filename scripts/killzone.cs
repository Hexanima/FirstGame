using Godot;
using System;

public partial class killzone : Area2D
{
	private Timer timer;
	public override void _Ready()
	{
		timer = GetNode<Timer>("Timer");
	}

	private void _on_body_entered(Node2D body)
	{
		GD.Print("Dead");
		Engine.TimeScale = 0.5;
		body.GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
		timer.Start();
	}

	private void _on_timer_timeout()
	{
		// Replace with function body.
		Engine.TimeScale = 1;
		this.GetTree().ReloadCurrentScene();
	}
}





