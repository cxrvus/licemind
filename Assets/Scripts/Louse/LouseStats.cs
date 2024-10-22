using UnityEngine;

public class LouseStats : MonoBehaviour
{
	public bool IsPlayer { get; private set; }


	static bool playerExists = false;

	void Awake()
	{
		if (!playerExists)
		{
			playerExists = true;
			IsPlayer = true;
		}

		// todo: spawn interactor instead of requiring
		var animator = GetComponent<LouseAnimator>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<LouseMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();
	}

	public void BecomePlayer() => IsPlayer = true;
	public void BecomeNpc() => IsPlayer = false;
}
