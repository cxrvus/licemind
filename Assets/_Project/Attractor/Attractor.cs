using System;
using UnityEngine;

public class Attractor : MonoBehaviour
{
	[NonSerialized] public AttractorStats stats;

	float _radius;
	public float Radius
	{
		get => _radius;
		private set
		{
			_radius = value;
			var diameter = value * 2;
			transform.GetChild(0).localScale = new Vector2(diameter, diameter);
		}
	}

	void Start()
	{
		GetComponent<SpriteRenderer>().sprite = stats.sprite;
		GetComponentsInChildren<SpriteRenderer>()[1].color = stats.auraColor;

		Radius = stats.maxRadius;
		name = stats.sprite.name;
	}

	void Update()
	{
		Radius -= stats.decayRate * Time.deltaTime;
		if (Radius < stats.minRadius) Destroy(gameObject);
	}
}
