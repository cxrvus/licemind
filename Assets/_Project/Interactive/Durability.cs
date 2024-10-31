using UnityEngine;

public class Durability
{
	public readonly GameObject gameObject;
	public readonly int valueCap;
	public readonly float minTransp;
	public SpriteRenderer Sprite { get; private set; }

	public int Value { get; private set; }

	public Durability(GameObject gameObject, int valueCap = 1, float minTransp = default)
	{
		this.gameObject = gameObject;
		this.valueCap = valueCap;
		this.minTransp = minTransp;

		Value = valueCap;
		Sprite = gameObject.GetComponent<SpriteRenderer>();
	}

	public void Damage(int amount)
	{
		Value -= amount;
		SetTransparency();
		if (Value <= 0) Object.Destroy(gameObject);
	}

	void SetTransparency()
	{
		var transpFactor = 1f - minTransp;
		var ratio = (float)Value / valueCap * transpFactor + minTransp;

		var color = Sprite.color;
		color.a = ratio;
		Sprite.color = color;
	}
}
