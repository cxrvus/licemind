using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LouseAI
{

	readonly Louse l;
	readonly Timer timer;

	LouseState state;

	public LouseAI(Louse louse)
	{
		l = louse;
		state = LouseState.Idle;
		timer = new Timer(louse.Stats.WalkInterval, true);
	}

    public void Tick()
	{
		if (l.IsInteracting) return;
		if (l.IsPlayer) throw new InvalidOperationException();

		timer.Push();
		if (!timer.Pop()) return;

		var nextState = state;

		if (state == LouseState.Walking)
		{
			l.Direction = RandomDirection();
			nextState = LouseState.Idle;
		}
		else if (state == LouseState.Idle)
		{
			l.IsMoving = false;
			nextState = LouseState.Walking;
		}
		
		l.Animate(state);

		state = nextState;
	}

	Vector2 RandomDirection()
	{
		// todo: attractors
		var randomDir = Random.onUnitSphere;
		randomDir.z = 0; // TODO: unneeded?
		return randomDir;
	}

	public void Reset()
	{
		timer.Reset();
	}
}
