using System;
using Godot;

public partial class player : CharacterBody2D
{
    [Export]
    private float Speed = 130.0f;

    [Export]
    private float JumpVelocity = -300.0f;

    [Export]
    private int MaxHealth = 100;
    private int CurrentHealth;

    private AnimatedSprite2D animatedSprite2D;
    private Timer timer;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    private void kill()
    {
        GD.Print("Dead");
        Engine.TimeScale = 0.5;
        this.GetNode<CollisionShape2D>("CollisionShape2D").QueueFree();
        timer.Start();
    }

    private void _on_timer_timeout()
    {
        // Replace with function body.
        Engine.TimeScale = 1;
        this.GetTree().ReloadCurrentScene();
    }

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        CurrentHealth = MaxHealth;
        timer = GetNode<Timer>("Timer");
        GD.Print(this.CurrentHealth);
    }

    public void Hurt(int damage)
    {
        this.CurrentHealth -= damage;

        if (this.CurrentHealth <= 0)
        {
            this.kill();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
            velocity.Y = JumpVelocity;

        // GetAxis es -1, 0, 1
        float direction = Input.GetAxis("move_left", "move_right");

        // Sprite rotation
        if (direction > 0)
        {
            animatedSprite2D.FlipH = false;
        }
        else if (direction < 0)
        {
            animatedSprite2D.FlipH = true;
        }

        // Animations
        if (IsOnFloor())
        {
            if (direction == 0)
            {
                animatedSprite2D.Play("idle");
            }
            else
            {
                animatedSprite2D.Play("run");
            }
        }
        else
        {
            animatedSprite2D.Play("jump");
        }

        // Movement
        if (direction != 0)
        {
            velocity.X = direction * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
