using System;
using System.Reflection;
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
    private Timer deadTimer;
    private Timer hurtTimer;
    private AudioStreamPlayer2D hurt;

    private bool isHurt = false;

    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public override void _Ready()
    {
        animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        CurrentHealth = MaxHealth;
        deadTimer = GetNode<Timer>("DeadTimer");
        hurtTimer = GetNode<Timer>("HurtTimer");

        hurt = GetNode<AudioStreamPlayer2D>("Hurt");
        GD.Print(this.CurrentHealth);
    }

    private void kill()
    {
        GD.Print("Dead");
        Engine.TimeScale = 0.5;
        deadTimer.Start();
    }

    private void _on_timer_timeout()
    {
        // Replace with function body.
        Engine.TimeScale = 1;
        this.GetTree().ReloadCurrentScene();
    }

    private void _on_hurt_timer_timeout()
    {
        isHurt = false;
    }

    public void Hurt(int damage)
    {
        this.CurrentHealth -= damage;
        hurt.Play();

        if (!IsAlive())
        {
            this.kill();
        }
        else
        {
            isHurt = true;
            hurtTimer.Start();
        }
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
            velocity.Y += gravity * (float)delta;

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor() && IsAlive())
            velocity.Y = JumpVelocity;

        // GetAxis es -1, 0, 1
        float direction = Input.GetAxis("move_left", "move_right");

        // Sprite rotation
        if (IsAlive())
        {
            if (direction > 0)
            {
                animatedSprite2D.FlipH = false;
            }
            else if (direction < 0)
            {
                animatedSprite2D.FlipH = true;
            }
        }

        // Animations
        if (IsAlive())
        {
            if (isHurt)
            {
                animatedSprite2D.Play("hurt");
            }
            else
            {
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
            }
        }
        else
        {
            animatedSprite2D.Play("dead");
        }

        // Movement
        if (direction != 0 && IsAlive())
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
