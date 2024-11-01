using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Durability
{
	public readonly GameObject gameObject;
	public readonly int valueCap;
	public readonly float minTransp;
	public SpriteRenderer Sprite { get; private set; }

	int _value;
	public int Value { get => _value; private set => _value = Math.Clamp(value, 0, valueCap); }

	public Durability(GameObject gameObject, int valueCap, float minTransp)
	{
		if (valueCap <= 0) throw new ArgumentOutOfRangeException();

		this.gameObject = gameObject;
		this.valueCap = valueCap;
		this.minTransp = minTransp;

		Value = valueCap;
		Sprite = gameObject.GetComponent<SpriteRenderer>();
	}

	public void Damage(int amount = 1)
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
