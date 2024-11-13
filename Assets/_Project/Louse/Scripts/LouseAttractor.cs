using UnityEngine;

public partial class Louse
{
	public AttractorBank attractors;

	public GameObject Spawn(AttractorStats attractorStats)
	{
		var instance = Instantiate(attractors.prefab);

		var position = new Vector3(transform.position.x, transform.position.y, Layers.ATTRACTOR);
		instance.transform.position = position;

		instance.GetComponentInChildren<Attractor>().stats = attractorStats;
		return instance;
	}
}
