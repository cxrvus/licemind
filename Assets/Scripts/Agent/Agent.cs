using System;
using UnityEngine;

public class Agent : MonoBehaviour
{
	public Vector2 direction;
	public bool IsPlayer { get; private set; }

	static bool playerExists = false;

	void Awake()
	{
		LinkEvents();

		if (!playerExists)
		{
			playerExists = true;
			BecomePlayer();
		}
	}

	void LinkEvents()
	{
		// todo: spawn interactor instead of requiring
		var animator = GetComponent<AgentAnimator>();
		var interactive = GetComponent<LouseInteractive>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<AgentMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();

		movement.OnMovement += animator.Movement;
		interactor.OnStartInteraction += animator.Interact;
	}

	public void BecomePlayer()
	{
		IsPlayer = true;
		PlayerSwitch();
	}

	public void BecomeNpc()
	{
		IsPlayer = false;
		PlayerSwitch();
	}

	void PlayerSwitch()
	{
		GetComponent<PlayerMovement>().enabled = IsPlayer;
	}
}
