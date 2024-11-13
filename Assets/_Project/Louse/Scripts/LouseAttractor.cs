using UnityEngine;

public partial class Louse
{
	public AttractorBank attractors;

	public GameObject Spawn(AttractorStats attractor)
	{
		var sprite = attractor.sprite;
		var instance = Instantiate(attractors.prefab);
		var position = new Vector3(transform.position.x, transform.position.y, Layers.ATTRACTOR);

		instance.transform.position = position;
		instance.GetComponent<SpriteRenderer>().sprite = sprite;
		instance.name = sprite.name;

		return instance;
	}
}
