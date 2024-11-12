using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	Timer walkTimer;
	Timer interactionTimer;
	Timer pheromoneCooldown;

	LState _state;
	public LState State
	{
		get => _state;
		private set
		{
			_state = value;
			Animate(value);
		}
	}
	LState? nextState;

	IEnumerator Loop()
	{
		walkTimer = new Timer(true, Stats.WalkInterval);
		interactionTimer = new Timer(false);
		pheromoneCooldown = new Timer(false, 1);

		for (;;)
		{
			if (State == LState.Interacting)
			{
				if (interactionTimer.ResetIfFinished()) nextState = LState.Idle;
			}
			else if (InteractionCheck()) Interact();
			else if (IsPlayer) PlayerTick();
			else NpcMovement();

			if (State != nextState) State = nextState ?? State;
			nextState = null;

			yield return null;
		}
	}

	void PlayerTick()
	{
		// todo: add cost
		// todo: LouseBaseStat for cost and cool-down duration
		// TODO: implement using Timer.IsRunning (2x)
		if (pheromoneCooldown.Elapsed > 0) pheromoneCooldown.ResetIfFinished();
		else if (Input.GetKey(KeyCode.Q))
		{
			attractors.pheromone.SpawnAt(transform);
			pheromoneCooldown.Tick();
		}

		PlayerMovement();
	}

	void Interact()
	{
		nextState = LState.Interacting;

		walkTimer.Reset();
		interactionTimer.Reset();
		interactionTimer.Max = target.stats.duration;

		Direction = Zero;
		Stats.Energy -= target.stats.effort;
		target.Interact(this);
	}

	public void Reset()
	{
		walkTimer.Reset();
	}
}
