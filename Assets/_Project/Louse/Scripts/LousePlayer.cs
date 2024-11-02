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
		if (Player) Player._isPlayer = false;
		_isPlayer = true;
		Player = this;

		State = LouseState.Idle;

		OnSwitchPlayer?.Invoke();
	}

	void SetupPlayer()
	{
		if (!Player) BecomePlayer();
	}


	Vector3 GetPlayerDirection() => new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
}
