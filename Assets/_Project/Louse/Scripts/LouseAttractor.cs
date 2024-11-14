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
}
