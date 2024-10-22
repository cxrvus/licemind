using UnityEngine;

public class LouseStats : MonoBehaviour
{
	public bool isPlayer;

	static bool playerExists = false;

	void Awake()
	{
		if (!playerExists)
		{
			playerExists = true;
			isPlayer = true;
		}

		// todo: spawn interactor instead of requiring
		var animator = GetComponent<LouseAnimator>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<LouseMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();
	}
}
