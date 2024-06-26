using Godot;
using System;

public partial class game_manager : Node
{
    private int score = 0;
    private Label scoreLabel;

    public override void _Ready()
    {
        scoreLabel = GetNode<Label>("ScoreLabel");
        scoreLabel.Text = $"No tienes monedas.";

    }

    public void AddPoint()
    {
        score += 1;

        if (score == 1)
        {
            scoreLabel.Text = $"Tienes {score} moneda.";
        }
        else
        {
            scoreLabel.Text = $"Tienes {score} monedas.";
        }
    }
}
