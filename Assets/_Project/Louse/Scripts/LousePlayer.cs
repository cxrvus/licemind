using System;
using UnityEngine;

public partial class Louse
{
	public static event Action OnSwitchPlayer;

	public static Louse Player { get; private set; }
	bool _isPlayer;
	public bool IsPlayer
	{
		set
		{
			if (!value) throw new ArgumentOutOfRangeException("Can only set IsPlayer to true");
			if (Player) Player._isPlayer = false;
			_isPlayer = true;
			Player = this;
			OnSwitchPlayer?.Invoke();
		}
		get => _isPlayer;
	}

	void SetupPlayer()
	{
		if (!Player) IsPlayer = true;
	}


	Vector3 GetPlayerDirection() => new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
}
