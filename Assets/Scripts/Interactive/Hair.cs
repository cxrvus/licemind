using UnityEngine;

public class Hair : Interactive
{
	public int maxDurability = 1;
	int durability;
	SpriteRenderer sprite;

	void Awake()
	{
		durability = maxDurability;
		sprite = GetComponent<SpriteRenderer>();
	}

	public override void Interact(Louse louse)
	{
		// todo: louse strength factor
		durability--;
		SetTransparency();
		if (durability <= 0) Destroy(gameObject);
	}

	void SetTransparency()
	{
		const float BASE_TRANSP = 0.8f;
		const float TRANSP_FACTOR = 1f - BASE_TRANSP;

		var ratio = (float)durability / maxDurability * TRANSP_FACTOR + BASE_TRANSP;

		var color = sprite.color;
		color.a = ratio;
		sprite.color = color;
	}
}
