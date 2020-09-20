using Godot;
using System;

public class Creeps : RigidBody2D
{
	
	[Export]
	public int MinSpeed = 150 ;
	[Export]
	public int MaxSpeed = 250 ;
	
	static private Random _random = new Random() ;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		/*
			1. get a list of extant animations 
			   in this case ["walk", "swim", "fly"]
			2. then need to pick a random number between
				0 and 2 to select one of these names from
				 the list (array indices start at 0). randi() % n selects a random integer between 0 and 
		*/
		var animSprite = GetNode<AnimatedSprite>("AnimatedSprite");
		var mobTypes = animSprite.Frames.GetAnimationNames();
		animSprite.Animation = mobTypes[_random.Next(0, mobTypes.Length)];
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	/*
		This method is called via the signal emmitted
		by the VisibilityNotifier2D node 
		This logic causes the the mobs to delete them
		selves....
	*/
	private void OnVisibilityNotifier2DScreenExited()
	{
		QueueFree();
	}


}


