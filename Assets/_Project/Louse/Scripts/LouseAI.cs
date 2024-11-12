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

	void SetupAI()
	{
		walkCycle = new Timer(true, Stats.WalkInterval);
		interactionCooldown = new Timer(false);
		pheromoneCooldown = new Timer(false, 1);
	}

	void Tick()
	{
		if (State == LState.Interacting)
		{
			if (interactionCooldown.IsFinished)
			{
				nextState = LState.Idle;
				if (!IsPlayer) walkCycle.Reset().Resume();
			}
		}
		else if (InteractionCheck()) Interact();
		else if (IsPlayer) PlayerTick();
		else NpcMovement();

		// todo: remove the need for State|nextState distinction
		if (State != nextState) State = nextState ?? State;
		nextState = null;
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

		if (!IsPlayer) walkCycle.Pause();
		interactionCooldown.Reset();
		interactionCooldown.Max = target.Stats.duration;
		interactionCooldown.Resume();

		Direction = Zero;
		Stats.Energy -= target.Stats.effort;
		target.Interact(this);
	}
}
