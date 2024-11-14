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
				State = LState.Idle;
				if (!IsPlayer) walkCycle.Reset().Resume();
			}
		}
		else if (InteractionCheck()) Interact();
		else if (IsPlayer) PlayerTick();
		else NpcMovement();
	}

	void PlayerTick()
	{
		// todo: add cost
		// todo: SO-field for cost and cool-down duration
		if (!pheromoneCooldown.IsRunning && Input.GetKey(KeyCode.Q))
		{
			Spawn(attractorBank.pheromone);
			pheromoneCooldown.Reset().Resume();
		}

		PlayerMovement();
	}

	void Interact()
	{
		State = LState.Interacting;

		if (!IsPlayer) walkCycle.Pause();
		interactionCooldown.Reset(target.Stats.duration).Resume();

		Direction = Zero;
		Stats.Energy -= target.Stats.effort;
		target.Interact(this);
	}
}
