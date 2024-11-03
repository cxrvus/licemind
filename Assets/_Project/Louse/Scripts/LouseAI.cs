using System;
using System.Collections;
using UnityEngine;

public partial class Louse
{
	Timer walkTimer;
	Timer interactionTimer;

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

	void SetupAi()
	{
		walkTimer = new Timer(cyclic: true, Stats.WalkInterval);
		interactionTimer = new Timer(cyclic: true);
	}

	IEnumerator Loop()
	{
		for (;;)
		{
			Tick();
			yield return null;
		}
	}

    void Tick()
	{
		if (State == LState.Interacting)
		{
			if (interactionTimer.PopOrPush()) nextState = LState.Idle;
			else {}
		}
		else if (InteractionCheck()) Interact();
		else if (IsPlayer) PlayerMovement();
		else NpcMovement();

		if (State != nextState) State = nextState ?? State;
		nextState = null;
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
