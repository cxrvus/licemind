using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	public GameObject defecationObject;
	public GameObject corpseObject;

	public LouseBaseStats baseStats;
	public LouseStats stats;

	public static event Action OnGameOver;

	void SetupStats() => stats = new LouseStats(this);

	IEnumerator ProcessStats()
	{
		for(;;)
		{
			stats.Advance();
			if (stats.Energy == 0 || stats.Age >= stats.AgeCap) Die();
			else if (stats.Digestion >= stats.DigestionCap) Defecate();
			// todo: add stat indicators (icons that blink proportional to urgency)
			// todo: implement blood icon transparency
			yield return new WaitForSeconds(stats.Interval);
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
		stats.Digestion = 0;
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
