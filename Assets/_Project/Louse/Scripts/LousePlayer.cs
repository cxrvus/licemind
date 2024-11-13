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
		State = LState.Idle;
		if (walkCycle.IsRunning) walkCycle.Pause();
		OnSwitchPlayer?.Invoke();
	}

	void BecomeNpc()
	{
		_isPlayer = false;
		State = LState.Idle;
		interactionCooldown.Reset();
		walkCycle.Reset().Resume();
	}

	void SetupPlayer()
	{
		if (Player) BecomeNpc();
		else BecomePlayer();
	}
}
