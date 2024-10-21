using UnityEngine;

public class Agent : MonoBehaviour
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
		var animator = GetComponent<AgentAnimator>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<AgentMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();
	}
}
