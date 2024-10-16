using UnityEngine;

public class Agent : MonoBehaviour
{
	public float speed;

	public bool isPlayer;
	public bool isInteracting;

	public const float BASE_SPEED = 100;

	static bool playerExists = false;

	Animator animator;

	public void Awake()
	{
		animator = GetComponent<Animator>();
		var interactor = GetComponentInChildren<Interactor>();

		if(!animator || !interactor) throw new MissingComponentException();

		if (!playerExists)
		{
			isPlayer = true;
			playerExists = true;
		}
	}

	public void PlayAnimation(string anim = "Idle", int layer = -1) => animator.Play(anim, layer);
}
