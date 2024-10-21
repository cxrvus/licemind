using UnityEngine;

public class LouseAnimator : MonoBehaviour
{
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		if(!animator) throw new MissingComponentException();
	}

	void Play(string anim, int layer = -1) => animator.Play(anim, layer);

	public void Movement(bool isMoving)
	{
		if(isMoving) Play("Walk");
		else Play("Idle");
	}

	public void Interact(Interactive target)
	{
		Play(target.louseAnimation.name);
	}
}
