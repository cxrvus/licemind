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
			if (value < 0) throw new ArgumentOutOfRangeException("max value must not be less than 0");
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
		if (IsFinished && IsRunning && !cyclical) Pause();
	}

	public void Resume()
	{
		if (!Clock.IsRunning) throw new InvalidOperationException("Clock has to be running to resume timers");
		if (IsRunning) throw new InvalidOperationException("cannot resume a timer that is already running");
		Clock.OnTick += Tick;
		IsRunning = true;
	}

	public void Pause()
	{
		if (!IsRunning) throw new InvalidOperationException("cannot pause a timer that is already paused");
		Clock.OnTick -= Tick;
		IsRunning = false;
	}

	public void Reset()
	{
		if (!cyclical && IsRunning) Pause();
		Elapsed = cyclical ? Elapsed % Max : 0;
	}
}
