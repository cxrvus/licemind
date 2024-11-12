using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	public BloodOverlay bloodOverlay;

	public LouseBaseStats baseStats;
	public LouseStats Stats { get; private set; }

	public static event Action OnGameOver;

	void SetupStats() => Stats = new LouseStats(this);

	IEnumerator ProcessStats()
	{
		for(;;)
		{
			Stats.PassiveUpdate();

			bloodOverlay.UpdateTransp();

			if (Stats.Energy == 0 || Stats.Age >= Stats.AgeCap) Die();
			else if (Stats.Digestion >= Stats.DigestionCap) Defecate();
			// todo: add stat indicators (icons that blink proportional to urgency)
			// todo: implement blood layer transparency
			yield return new WaitForSeconds(Stats.UpdateInterval);
		}
	}

	void Defecate()
	{
		attractors.defecation.SpawnAt(transform);
		Stats.Digestion = 0;
	}

	void Die()
	{
		lice.Remove(this);

		var corpse = attractors.corpse.SpawnAt(transform);
		corpse.transform.rotation = transform.rotation;

		if (Count == 0) OnGameOver?.Invoke();
		else if (IsPlayer)
		{
			if (target) target.HidePrompt();
			lice[Count-1].BecomePlayer();
		}

		Destroy(gameObject);
	}
}
