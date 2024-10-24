using UnityEngine;

public class Hair : Interactive
{
	int durability;
	SpriteRenderer sprite;

	void Awake()
	{
		durability = HairBaseStats.durabilityCap;
		sprite = GetComponent<SpriteRenderer>();
	}

	public override void Interact(LouseStats interactor)
	{
		var strength = interactor.Strength;
		durability -= strength;
		interactor.Energy -= strength;
		SetTransparency();
		if (durability <= 0) Destroy(gameObject);
	}

	void SetTransparency()
	{
		var ratio = (float)durability / HairBaseStats.durabilityCap * HairBaseStats.transpFactor + HairBaseStats.baseTransp;

		var color = sprite.color;
		color.a = ratio;
		sprite.color = color;
	}
}
