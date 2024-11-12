using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Durability
{
	public readonly GameObject gameObject;
	public readonly int valueCap;
	public readonly float minTransp;
	public readonly float transpFactor;
	readonly SpriteRenderer sprite;

	int _value;
	public int Value
	{
		get => _value;
		set
		{
			_value = Math.Clamp(value, 0, valueCap);
			SetTransparency();
			if (Value <= 0) Object.Destroy(gameObject);
		}
	}

	public Durability(GameObject gameObject, int valueCap, float minTransp)
	{
		if (valueCap <= 0) throw new ArgumentOutOfRangeException();

		this.gameObject = gameObject;
		this.valueCap = valueCap;
		this.minTransp = minTransp;

		transpFactor = 1f - minTransp;

		Value = valueCap;
		sprite = gameObject.GetComponent<SpriteRenderer>();
	}

	public void Damage(int amount = 1) => Value -= amount;

	void SetTransparency()
	{
		if (!sprite) return;

		var ratio = (float)Value / valueCap * transpFactor + minTransp;

		var color = sprite.color;
		color.a = ratio;
		sprite.color = color;
	}
}
