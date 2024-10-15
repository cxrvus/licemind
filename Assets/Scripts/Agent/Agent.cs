using UnityEngine;

public class Agent : MonoBehaviour
{
	public float speed;

	public bool isPlayer;
	public bool isInteracting;

	public const float BASE_SPEED = 100;

	Animator animator;

	public void Awake()
	{
		animator = GetComponent<Animator>();
		var interactor = GetComponentInChildren<Interactor>();

		if(!animator || !interactor) { throw new MissingComponentException(); }
	}

	public void PlayAnimation(string anim = "Idle", int layer = -1) => animator.Play(anim, layer);
}
