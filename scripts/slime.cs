using Godot;
using System;

public partial class slime : Node2D
{
	private RayCast2D rayCastRight;
	private RayCast2D rayCastLeft;
	private AnimatedSprite2D animatedSprite2D;

	private float direction = 1;
	private float SPEED = 60;

	public override void _Ready()
	{
		rayCastRight = GetNode<RayCast2D>("RayCastRight");
		rayCastLeft = GetNode<RayCast2D>("RayCastLeft");
		animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (rayCastRight.IsColliding()) {
			direction = -1;
			animatedSprite2D.FlipH = true;
		} else if (rayCastLeft.IsColliding()) {
			direction = 1;
			animatedSprite2D.FlipH = false;
		}

		Vector2 pos = this.Position;
		this.Position = new Vector2(pos.X + SPEED * direction * (float)delta, pos.Y);
	}
}
