using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouseStats : MonoBehaviour
{
	static readonly List<LouseStats> lice = new ();
	public static int LouseCount { get { return lice.Count; } }

	public static LouseStats PlayerStats { get; private set; }
	bool _isPlayer;
	public bool IsPlayer { get { return _isPlayer; }
	set {
			if (!value) throw new ArgumentOutOfRangeException("Can only set IsPlayer to true");
			if (PlayerStats) PlayerStats._isPlayer = false;
			_isPlayer = true;
			PlayerStats = this;
			OnSwitchPlayer?.Invoke();
			OnUpdateStats?.Invoke();
		}
	}
	public static event Action OnSwitchPlayer;
	public static event Action OnUpdateStats;
	public static event Action OnGameOver;


	int _energy;
	public int EnergyCap { get; private set; }
	public int Energy { get { return _energy; } set { _energy = Math.Clamp(value, 0, EnergyCap); UpdateStats(); } }

	public int Strength { get; private set; }

	public int Speed { get; private set; }
	LouseMovement _movement;
	bool IsMoving { get { return _movement.IsMoving; } }


	void Start()
	{
		_movement = GetComponent<LouseMovement>();
		lice.Add(this);

		if (!PlayerStats) IsPlayer = true;

		SetupStats();
		StartCoroutine(Metabolism());
	}

	IEnumerator Metabolism()
	{
		for(;;)
		{
			var depletion = IsPlayer ? IsMoving ? LouseBaseStats.metabolismPlayerWalk : LouseBaseStats.metabolismPlayerIdle : LouseBaseStats.metabolismNpcIdle;
			Energy -= depletion;
			yield return new WaitForSeconds(1);
		}
	}

	void SetupStats()
	{
		// idea: add random value variations
		EnergyCap = LouseBaseStats.energyCap;
		Energy = EnergyCap;
		Strength = LouseBaseStats.strength;
		Speed = LouseBaseStats.speed;
	}

	void UpdateStats()
	{
		DeathCheck();
		OnUpdateStats?.Invoke();
	}

	void DeathCheck()
	{
		if (Energy == 0) {
			Die();
		}
	}

	void Die()
	{
		// todo: spawn corpse
		lice.Remove(this);
		if (LouseCount == 0) OnGameOver?.Invoke();
		else lice[LouseCount].IsPlayer = true;
		Destroy(gameObject);
	}
}
