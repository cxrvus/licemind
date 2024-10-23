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


	float _energy;
	public float MaxEnergy { get; private set; }
	public float Energy { get { return _energy; } set { _energy = Math.Clamp(value, 0, MaxEnergy); UpdateStats(); } }

	public float Strength { get; private set; }

	void Awake()
	{
		// todo: spawn interactor instead of requiring
		var animator = GetComponent<LouseAnimator>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<LouseMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();

		lice.Add(this);
	}

	void Start()
	{
		if (!PlayerStats) IsPlayer = true;

		MaxEnergy = 10;
		Energy = MaxEnergy;

		StartCoroutine(Metabolism());
	}

	IEnumerator Metabolism()
	{
		for(;;)
		{
			Energy -= 1;
			yield return new WaitForSeconds(1);
		}
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
