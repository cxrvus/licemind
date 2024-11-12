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
	public float Elapsed { get; private set; }
	public bool IsFinished { get => Elapsed >= Max; }

	public bool IsRunning { get; private set; }

	public readonly bool cyclical;

	public Timer(bool cyclical, float max = 0)
	{
		this.cyclical = cyclical;
		Max = max;
	}

	void Tick()
	{
		Elapsed += Time.deltaTime;
		if (IsFinished && !cyclical) Pause();
	}

	public void Resume()
	{
		if (!Clock.IsRunning) throw new InvalidOperationException();
		Clock.OnTick += Tick;
		IsRunning = true;
	}

	public void Pause()
	{
		Clock.OnTick -= Tick;
		IsRunning = false;
	}

	public void Reset()
	{
		if (!cyclical) Pause();
		Elapsed = cyclical ? Elapsed % Max : 0;
	}
}
