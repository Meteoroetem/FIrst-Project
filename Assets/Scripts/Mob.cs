using Godot;
using System;

public partial class Mob : RigidBody2D
{

    public void OnScreenExit() => QueueFree();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        AnimatedSprite2D spriteAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        spriteAnim.Play();
        string[] anims = spriteAnim.SpriteFrames.GetAnimationNames();
        spriteAnim.Animation = anims[GD.Randi() % anims.Length];
        
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
