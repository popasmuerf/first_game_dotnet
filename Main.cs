using Godot;
using System;

public class Main : Node
{
	[Export]
	public PackedScene Mob ;
	
	private int _score  ;
	
	private Random _random = new Random() ;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//NewGame();
	}


	// We'll use this later because C# doesn't support GDScript's randi().
	private float RandRange(float min, float max)
	{
		return (float)_random.NextDouble() * (max - min) + min;
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	private void GameOver()
	{
		GetNode<Timer>("MobTimer").Stop() ;
		GetNode<Timer>("ScoreTimer").Stop() ;
		GetNode<CanvasLayer>("CanvasLayer").ShowGameOver();
		var music = GetNode<AudioStreamPlayer>("Music");
		music.Stop() ;
		
		var deathSound = GetNode<AudioStreamPlayer>("DeathSound");
		deathSound.Play() ;
		
		// Replace with function body.
	}
	/*
	private void NewGame()
	{
		_score = 0 ;
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition") ;
		player.Start(startPosition.Position);
		GetNode<Timer>("StartTimer").Start() ;
		
		var hud = GetNode<CanvasLayer>("CanvasLayer");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");
	}
	*/
	
	private void OnStartTimerTimeout()
	{
		GetNode<Timer>("MobTimer").Start() ;
		GetNode<Timer>("ScoreTimer").Start() ;
		GetNode<CanvasLayer>("CanvasLayer").UpdateScore(_score);
	}
	
	
	private void OnScoreTimerTimeout()
	{
		 _score = _score + 1 ;
		GetNode<CanvasLayer>("CanvasLayer").UpdateScore(_score);
	}


	
	private void OnMobTimerTimeout()
	{
		// Choose a random location on Path2D.
		var mobSpawnLocation = GetNode<PathFollow2D>("MobPath/MobSpawnLocation");
		mobSpawnLocation.Offset = _random.Next();
	
		// Create a Mob instance and add it to the scene.
		var mobInstance = (RigidBody2D)Mob.Instance();
		AddChild(mobInstance);
	
		// Set the mob's direction perpendicular to the path direction.
		float direction = (float) mobSpawnLocation.Rotation + Mathf.Pi / 2;
	
		// Set the mob's position to a random location.
		mobInstance.Position = mobSpawnLocation.Position;
	
		// Add some randomness to the direction.
		direction += (float) GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
		mobInstance.Rotation = direction;
	
		// Choose the velocity.
		mobInstance.LinearVelocity = new Vector2(   (float) GD.RandRange(150f, 250f)    , 0).Rotated(direction);
	}
	
	private void NewGame()
	{
			_score = 0 ;
		var player = GetNode<Player>("Player");
		var startPosition = GetNode<Position2D>("StartPosition") ;
		player.Start(startPosition.Position);
		GetNode<Timer>("StartTimer").Start() ;
		
		var hud = GetNode<CanvasLayer>("CanvasLayer");
		hud.UpdateScore(_score);
		hud.ShowMessage("Get Ready!");
		hud.ShowMessage(" ");
		
		var music = GetNode<AudioStreamPlayer>("Music");
		music.Play() ;
	}
	
}






