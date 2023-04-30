using Godot;
using System;

public partial class Player : Area2D
{
    [Signal]
    public delegate void HitEventHandler();
	[Export]public int speed = 400;
	public Vector2 screenSize;

    public void OnBodyEntered(PhysicsBody2D body)
    {
        Hide();
        EmitSignal(SignalName.Hit);
        GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
    }

    public void Start(Vector2 pos){
        Position = pos;
        Show();
        GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
    }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        screenSize = GetViewportRect().Size;
        Hide();
        Start(Vector2.Zero);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 velocity = Vector2.Zero;

        if (Input.IsActionPressed("moveRight"))
            velocity.X += 1;
        
        if (Input.IsActionPressed("moveLeft"))
            velocity.X -= 1;
        
        if (Input.IsActionPressed("moveDown"))
            velocity.Y += 1;
        
        if (Input.IsActionPressed("moveUp")) 
            velocity.Y -= 1;

        AnimatedSprite2D spriteAnim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        if (velocity.Length() > 0)
        {
            velocity = velocity.Normalized() * speed;
            spriteAnim.Play();
        }
        else spriteAnim.Stop();

        Position += velocity * (float)delta;
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y)
        );

        if (velocity.X != 0)
        {
            spriteAnim.Animation = "Walk";
            spriteAnim.FlipH = velocity.X < 0;
        }
        else if (velocity.Y != 0)
        {
            spriteAnim.Animation = 
                velocity.Y > 0 ? "Down" : "Up";
        }
	}

    
}
