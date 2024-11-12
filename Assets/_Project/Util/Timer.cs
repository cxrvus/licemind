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

	public readonly bool cyclic;

	public Timer(bool cyclic, float max = 0)
	{
		this.cyclic = cyclic;
		Max = max;
	}

	void Tick()
	{
		Elapsed += Time.deltaTime;
		if (IsFinished && !cyclic) Pause();
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
		if (!cyclic) Pause();
		Elapsed = cyclic ? Elapsed % Max : 0;
	}
}
