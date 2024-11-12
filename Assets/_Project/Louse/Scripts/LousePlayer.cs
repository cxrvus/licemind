using System;
using UnityEngine;

public partial class Louse
{
	public static event Action OnSwitchPlayer;

	public static Louse Player { get; private set; }
	bool _isPlayer;
	public bool IsPlayer { get => _isPlayer; }
	public void BecomePlayer()
	{
		if (Player) Player.BecomeNpc();
		Player = this;
		_isPlayer = true;
		nextState = LState.Idle;
		if (walkCycle.IsRunning) walkCycle.Pause();
		OnSwitchPlayer?.Invoke();
	}

	void BecomeNpc()
	{
		_isPlayer = false;
		nextState = LState.Idle;
		interactionCooldown.Reset();
		walkCycle.Reset().Resume();
	}

	void SetupPlayer()
	{
		if (Player) BecomeNpc();
		else BecomePlayer();
	}
}
