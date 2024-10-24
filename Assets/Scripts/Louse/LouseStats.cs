using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouseStats : MonoBehaviour
{
	public GameObject defecationObject;
	public GameObject corpseObject;

	static readonly List<LouseStats> lice = new ();
	public static int LouseCount { get => lice.Count; }

	public static LouseStats PlayerStats { get; private set; }
	bool _isPlayer;
	public bool IsPlayer 
	{
		set
		{
			if (!value) throw new ArgumentOutOfRangeException("Can only set IsPlayer to true");
			if (PlayerStats) PlayerStats._isPlayer = false;
			_isPlayer = true;
			PlayerStats = this;
			OnSwitchPlayer?.Invoke();
			OnUpdateStats?.Invoke();
		}
		get => _isPlayer;
	}
	public static event Action OnSwitchPlayer;
	public static event Action OnUpdateStats;
	public static event Action OnGameOver;


	int _energy;
	public int EnergyCap { get; private set; }
	public int Energy { get => _energy; set { _energy = Math.Clamp(value, 0, EnergyCap); UpdateStats(); } }

	int _age;
	public int AgeCap { get; private set; }
	public int Age { get => _age; private set { _age = value; UpdateStats(); } }

	int _digestion;
	public int DigestionCap { get; private set; }
	public int Digestion { get => _digestion; set { _digestion = Math.Clamp(value, 0, DigestionCap); UpdateStats(); } }

	public int Strength { get; private set; }

	public int Speed { get; private set; }
	LouseMovement _movement;
	bool IsMoving { get => _movement.IsMoving; }

	void Start()
	{
		_movement = GetComponent<LouseMovement>();
		lice.Add(this);

		if (!PlayerStats) IsPlayer = true;

		SetupStats();
		StartCoroutine(ProcessStats());
	}

	void SetupStats()
	{
		EnergyCap = LouseBaseStats.energyCap;
		_energy = EnergyCap;
		AgeCap = LouseBaseStats.ageCap;
		DigestionCap = LouseBaseStats.digestionCap;
		Strength = LouseBaseStats.strength;
		Speed = LouseBaseStats.speed;
		// idea: add random value variations
	}


	IEnumerator ProcessStats()
	{
		for(;;)
		{
			var depletion = IsPlayer ? IsMoving ? LouseBaseStats.metabolismPlayerWalk : LouseBaseStats.metabolismPlayerIdle : LouseBaseStats.metabolismNpcIdle;
			Energy -= depletion;
			Digestion += IsMoving ? LouseBaseStats.digestionWalk : LouseBaseStats.digestionIdle;
			Age++;
			yield return new WaitForSeconds(LouseBaseStats.updateInterval);
		}
	}

	void UpdateStats()
	{
		OnUpdateStats?.Invoke();
		if (Energy == 0 || Age >= AgeCap) Die();
		if (Digestion >= DigestionCap) Defecate();
	}

	void Defecate()
	{
		SpawnAttractor(defecationObject);
		Digestion = 0;
	}

	void SpawnAttractor(GameObject prefab)
	{
		var gameObject = Instantiate(prefab);
		var position = new Vector3(transform.position.x, transform.position.y, -2); // idea: parameterize Z using SO
		gameObject.transform.position = position;
	}

	void Die()
	{
		lice.Remove(this);
		SpawnAttractor(corpseObject);
		if (LouseCount == 0) OnGameOver?.Invoke();
		else lice[LouseCount].IsPlayer = true;
		Destroy(gameObject);
	}
}
