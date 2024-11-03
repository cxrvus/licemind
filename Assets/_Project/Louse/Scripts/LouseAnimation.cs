using System;
using UnityEngine;

public partial class Louse
{
	Animator animator;
	LouseState lastState;

	public void Animate(LouseState state)
	{
		if (lastState != state) lastState = state;
		else return;

		var animation = state switch
		{
			LouseState.Idle => "Idle",
			LouseState.Walking => "Walk",
			LouseState.Interacting => target.stats.louseAnimation.name,
			_ => throw new NotImplementedException()
		};

		animator.Play(animation);
	}
}
