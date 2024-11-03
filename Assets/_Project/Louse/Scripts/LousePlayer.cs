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
		ai.Reset();
		Animate(LouseState.Idle);
		OnSwitchPlayer?.Invoke();
	}

	void BecomeNpc() => _isPlayer = false;

	void SetupPlayer()
	{
		if (!Player) BecomePlayer();
	}
}
