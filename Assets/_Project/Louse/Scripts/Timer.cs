using UnityEngine;

public class Timer
{
	public readonly float max;
	public readonly bool cyclic;

	public Timer(float max, bool cyclic)
	{
		this.max = max;
		this.cyclic = cyclic;
	}

	public float Elapsed { get; private set; }

	public void Push(float time) => Elapsed += time;
	public void Push() => Push(Time.deltaTime);
	public void Reset() => Elapsed = 0;
	public bool Pop()
	{
		if (Elapsed >= max)
		{
			if (cyclic) Elapsed -= max;
			else Reset();
			return true;
		}
		else return false;
	}
}
