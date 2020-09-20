using Godot;
using System;

public class CanvasLayer : Godot.CanvasLayer
{
	/*
		Apparently all I have some options w/r to
		specifying what methods get called 
		when we declare signals....If this 
		scene is instanced by the Main scene...then
		I can delegate directly to Main.StartGame()
		as opposed to signal set up via the IDE
	*/
	[Signal]
	public delegate void StartGame() ;
	// Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}
	
	
	public void ShowMessage(String text)
	{
		var message = GetNode<Label>("Message");
		message.Text = text ;
		message.Show();
		GetNode<Timer>("MessageTimer").Start() ;
		message.Hide() ;
	}
	
	async public void ShowGameOver()
	{
		ShowMessage("Game Over") ;
		var messageTimer = GetNode<Timer>("MessageTimer");
		await ToSignal(messageTimer,"timeout") ;
		
		var message = GetNode<Label>("Message")  ;
		message.Text = "Dodge the \nBloops!!!";
		message.Show() ;
		
		await ToSignal(GetTree().CreateTimer(1),"timeout") ;
		GetNode<Button>("StartButton").Show() ;
	}
	
	public void UpdateScore(int score)
	{
		GetNode<Label>("ScoreLabel").Text = score.ToString();
	}

	public void OnStartButtonPressed()
	{
		GetNode<Button>("StartButton").Hide();
		EmitSignal("StartGame");
	}
	
	public void OnMessageTimerTimeout()
	{
		GetNode<Label>("Message").Hide();
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

