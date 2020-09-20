using Godot;
using System;



public class Player : Area2D
{
	
[Signal]
public delegate void Hit() ;

	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	/**
		So what do we need to set up first and globall?
		1. Set up a 2D vector representation of the 
		   space you are operating in....
		2. Set up base scalar for "speed" of
		   player movement
		3. Set up a Vector to represent 
		   the players direction and posistion
	*/
	[Export]
	public int speed = 400 ;
	Vector2  _screenSize ;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_screenSize = GetViewport().Size  ;
		Hide() ;
	}

// Called every frame. 'delta' is the elapsed time since the previous frame.
  public override void _Process(float delta)
  {
		// This is the player's current posstion vector
  		var velocity =  new Vector2()  ; 
		
 if (Input.IsActionPressed("ui_right"))
	{
		velocity.x += 1;
	}

	if (Input.IsActionPressed("ui_left"))
	{
		velocity.x -= 1;
	}

	if (Input.IsActionPressed("ui_down"))
	{
		velocity.y += 1;
	}

	if (Input.IsActionPressed("ui_up"))
	{
		velocity.y -= 1;
	}
	
	var animatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
	if(velocity.Length() > 0){
		velocity = velocity.Normalized() * speed ;
		animatedSprite.Play();
	}else{
		animatedSprite.Stop() ;
	}
	/**
	Yeah, we updated the Vector2 that
	is a representation within the current
	frame, but if you run the code, but nothing
	moves...that's becfause you need to update 
	the Area2D.Posistion object with your new vector
	in order for the framework to update the rendered
	posistion .
	*/
	Position += velocity * delta ;
	Position = new Vector2(
			x: Mathf.Clamp(Position.x, 0, _screenSize.x),
	y: Mathf.Clamp(Position.y, 0, _screenSize.y)
	);
	/**
		Now that the player can move, 
		we need to change which animation 
		the AnimatedSprite is playing based
		 on its direction. We have the "walk"
		 animation, which shows the player
		 walking to the right. This animation should be 
		flipped horizontally using the flip_h property 
		for left movement. We also have the "up" animation,
		 which should be flipped vertically with flip_v for downward movement. Let's place this code at the end of the _process() function:
	*/
	
	if (velocity.x != 0)
		{
			animatedSprite.Animation = "walk";
			animatedSprite.FlipV = false;
			// See the note below about boolean assignment
			animatedSprite.FlipH = velocity.x < 0;
		}
	else if (velocity.y != 0)
		{
			animatedSprite.Animation = "up";
			animatedSprite.FlipV = velocity.y > 0;
		}
  }


	private void OnPlayerBodyEntered(PhysicsBody2D body)
	{
		Hide() ;
		EmitSignal("Hit");
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred("disabled", true);
		// Replace with function body.
	}
	
	
	
	public void Start(Vector2 pos)
	{
		Position = pos ;
		Show() ;
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false ;
	}

}



