using Godot;
using System;

public partial class hud : CanvasLayer
	{
	// Don't forget to rebuild the project so the editor knows about the new signal.

	[Signal]
	public delegate void StartGameEventHandler();


	public void ShowMessage(string text)
	{
	var message = GetNode<Label>("Message");
	message.Text = text;
	message.Show();

	GetNode<Timer>("MessageTimer").Start();
	}

	async public void ShowGameOver()
	{
	ShowMessage("Perdeste");

	var messageTimer = GetNode<Timer>("MessageTimer");
	await ToSignal(messageTimer, Timer.SignalName.Timeout);

	var message = GetNode<Label>("Message");
	message.Text = "Seu pato!";
	message.Show();

	await ToSignal(GetTree().CreateTimer(1.0), SceneTreeTimer.SignalName.Timeout);
	GetNode<Button>("StartButton").Show();
	GetNode<Button>("QuitButton").Show();
	}


	public void UpdateScore(int score)
	{
	GetNode<Label>("ScoreLabel").Text = score.ToString();
	}


	private void OnStartButtonPressed()
	{
	GetNode<Button>("StartButton").Hide();
	GetNode<Label>("Message").Hide();
	GetNode<Button>("QuitButton").Hide();
	EmitSignal(SignalName.StartGame);
	}

	private void OnMessageTimerTimeout()
	{
	GetNode<Label>("Message").Hide();
	}

	private void OnQuitButtonPressed()
	{
		GetTree().Quit();
	// Replace with function body.
	}


}








