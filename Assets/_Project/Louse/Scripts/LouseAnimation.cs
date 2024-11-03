using System;
using UnityEngine;

public partial class Louse
{
	Animator animator;

	void AnimateWalk() => animator.Play("Walk");
	void AnimateIdle() => animator.Play("Idle");
	void AnimateInteraction() => animator.Play(target.stats.louseAnimation.name);
}
