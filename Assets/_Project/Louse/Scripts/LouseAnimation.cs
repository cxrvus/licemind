using System;
using UnityEngine;

public partial class Louse
{
	Animator animator;
	void UpdateAnimation()
	{
		var animation = State switch
		{
			LouseState.Idle => "Idle",
			LouseState.Walking => "Walk",
			LouseState.Interacting => target.stats.louseAnimation.name,
			_ => throw new NotImplementedException(),
		};
		
		animator.Play(animation);
	}
}
