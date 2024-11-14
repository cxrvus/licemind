using System.Collections.Generic;
using UnityEngine;

public partial class Louse
{
	public AttractorBank attractorBank;

	public GameObject Spawn(AttractorStats attractorStats)
	{
		var instance = Instantiate(attractorBank.prefab);

		var position = new Vector3(transform.position.x, transform.position.y, Layers.ATTRACTOR);
		instance.transform.position = position;

		instance.GetComponentInChildren<Attractor>().stats = attractorStats;
		return instance;
	}

	readonly List<Attractor> nearbyAttractors = new();

	void EditNearbyAttractors(Collider2D collider, bool add)
	{
		var attractor = collider.GetComponentInParent<Attractor>();
		if (!IsPlayer && attractor)
		{
			if (add) nearbyAttractors.Add(attractor);
			else nearbyAttractors.Remove(attractor);
		}
	}

	void OnTriggerEnter2D(Collider2D collider) => EditNearbyAttractors(collider, true);
	void OnTriggerExit2D(Collider2D collider) => EditNearbyAttractors(collider, false);
}
