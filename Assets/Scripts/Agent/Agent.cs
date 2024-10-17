using UnityEngine;

public class Agent : MonoBehaviour
{
	public float speed;

	// todo: propagate these using events instead of direct coupling
	public bool isPlayer;
	public bool isInteracting;
	public Vector2 direction;

	public const float BASE_SPEED = 100;

	static bool playerExists = false;

	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
		
		// todo: spawn interactor instead of requiring
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
