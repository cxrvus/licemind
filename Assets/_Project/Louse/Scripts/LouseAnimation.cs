using System;
using UnityEngine;

public partial class Louse
{
	Animator animator;

	public void Animate(LState state)
	{
		var animation = state switch
		{
			LState.Idle => "Idle",
			LState.Walking => "Walk",
			LState.Interacting => target.Stats.louseAnimation.name,
			_ => null
		};

		if (animation is not null) animator.Play(animation);
	}
}
