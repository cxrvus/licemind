using System;
using UnityEngine;

public class Durability : MonoBehaviour
{
	int valueCap;
	public int Value { get; private set; }
	bool isSetup;

	SpriteRenderer sprite;

	void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}

	public void Setup(int valueCap)
	{
		this.valueCap = valueCap;
		isSetup = true;
	}

	public void Damage(int amount)
	{
		if (!isSetup) throw new Exception("Durability component needs to be set up before interaction");

		Value -= amount;
		SetTransparency();
		if (Value <= 0) Destroy(gameObject);
	}

	const float BASE_TRANSP = 0.8f;
	const float TRANSP_FACTOR = 1f - BASE_TRANSP;
	void SetTransparency()
	{
		var ratio = (float)Value / valueCap * TRANSP_FACTOR + BASE_TRANSP;

		var color = sprite.color;
		color.a = ratio;
		sprite.color = color;
	}
}
