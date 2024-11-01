using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	public GameObject defecationObject;
	public GameObject corpseObject;

	public LouseBaseStats baseStats;
	public LouseStats Stats { get; private set; }

	public static event Action OnGameOver;

	void SetupStats() => Stats = new LouseStats(this);

	IEnumerator ProcessStats()
	{
		for(;;)
		{
			Stats.Advance();
			if (Stats.Energy == 0 || Stats.Age >= Stats.AgeCap) Die();
			else if (Stats.Digestion >= Stats.DigestionCap) Defecate();
			// todo: add stat indicators (icons that blink proportional to urgency)
			// todo: implement blood layer transparency
			yield return new WaitForSeconds(Stats.Interval);
		}
	}

	GameObject SpawnAttractor(GameObject prefab)
	{
		var instance = Instantiate(prefab);
		var position = new Vector3(transform.position.x, transform.position.y, Layers.ATTRACTOR);
		instance.transform.position = position;
		return instance;
	}

	void Defecate()
	{
		SpawnAttractor(defecationObject);
		Stats.Digestion = 0;
	}

	void Die()
	{
		lice.Remove(this);

		var corpse = SpawnAttractor(corpseObject);
		corpse.transform.rotation = transform.rotation;

		if (Count == 0) OnGameOver?.Invoke();
		else if (IsPlayer)
		{
			if (target) target.HidePrompt();
			lice[Count-1].IsPlayer = true;
		}

		Destroy(gameObject);
	}
}
