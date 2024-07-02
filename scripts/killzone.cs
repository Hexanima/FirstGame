using System;
using Godot;

public partial class killzone : Area2D
{
    [Export]
    private int Damage = 0;

    private void _on_body_entered(Node2D body)
    {
        if (body is player)
        {
            ((player)body).Hurt(this.Damage);
        }
    }
}
