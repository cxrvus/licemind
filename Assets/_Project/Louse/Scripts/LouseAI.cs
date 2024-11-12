using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	Timer walkCycle;
	Timer interactionCooldown;
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
		walkCycle = new Timer(true, Stats.WalkInterval);
		interactionCooldown = new Timer(false);
		pheromoneCooldown = new Timer(false, 1);

		for (;;)
		{
			if (State == LState.Interacting)
			{
				if (interactionCooldown.IsFinished)
				{
					nextState = LState.Idle;
					walkCycle.Reset();
					walkCycle.Resume();
				}
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
		// todo: SO-field for cost and cool-down duration
		if (pheromoneCooldown.IsFinished && Input.GetKey(KeyCode.Q))
		{
			attractors.pheromone.SpawnAt(transform);
		}

		PlayerMovement();
	}

	void Interact()
	{
		nextState = LState.Interacting;

		walkCycle.Pause();
		interactionCooldown.Reset();
		interactionCooldown.Max = target.stats.duration;
		interactionCooldown.Resume();

		Direction = Zero;
		Stats.Energy -= target.stats.effort;
		target.Interact(this);
	}

	public void Reset()
	{
		walkCycle.Reset();
	}
}
