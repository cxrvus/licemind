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
	public bool Finished  { get => Elapsed >= Max; }

	public bool IsRunning { get; private set; }

	public readonly bool cyclic;

	public Timer(bool cyclic, float max = 0)
	{
		this.cyclic = cyclic;
		Max = max;
		Resume();
	}

	void Tick()
	{
		Elapsed += Time.deltaTime;
		if (Finished && !cyclic) IsRunning = false;
	}

	void Resume()
	{
		// todo: throw if Clock is not running
		Clock.OnTick += Tick;
		IsRunning = true;
	}

	void Pause()
	{
		Clock.OnTick -= Tick;
		IsRunning = false;
	}

	public bool ResetIfFinished()
	{
		if (Finished) Reset();
		return Finished;
	}
	public void Reset() => Elapsed = cyclic ? Elapsed % Max : 0;
}
