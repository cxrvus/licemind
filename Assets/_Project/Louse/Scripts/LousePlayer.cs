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
		if (npcMovement != null) StopCoroutine(npcMovement);
		if (Player) Player.BecomeNpc();
		Player = this;
		_isPlayer = true;
		AnimateIdle();
		OnSwitchPlayer?.Invoke();
	}

	void BecomeNpc()
	{
		npcMovement = Player.StartCoroutine(NpcMovement());
		_isPlayer = false;
	}

	void SetupPlayer()
	{
		if (!Player) BecomePlayer();
	}
}
