using System;
using UnityEngine;

public class Timer
{
	float _max;
	public float Max
	{
		get => _max;
		set
		{
			if (value < 0) throw new ArgumentOutOfRangeException();
			_max = value;
		}
	}

	public readonly bool cyclic;

	public Timer(bool cyclic, float max = 0)
	{
		this.cyclic = cyclic;
		Max = max;
	}

	public float Elapsed { get; private set; }

	public void Push() => Push(Time.deltaTime);
	public void Push(float time)
	{
		if (Max == 0) throw new InvalidOperationException();
		Elapsed += time;
	}
	public bool Pop()
	{
		if (Max == 0) throw new InvalidOperationException();
		if (Elapsed >= Max)
		{
			if (cyclic) Elapsed -= Max;
			else Reset();
			return true;
		}
		else return false;
	}
	public bool PopOrPush()
	{
		if (Pop()) return true;
		else Push();
		return false;
	}
	public void Reset() => Elapsed = 0;
}
