using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Agent agent;
	Animator animator;

	public bool isInteracting;
	public Interactive ClosestInteractive { get; private set; }
	readonly List<Interactive> interactives = new ();
	bool IsHovering { get { return interactives.Count > 0; } }

	public event Action OnInteractionStart;
	public event Action OnInteractionStop;

	public void Start()
	{
		agent = GetComponentInParent<Agent>();
		animator = GetComponentInParent<Animator>();

		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		for(;;)
		{
			bool interactionKeyPressed = Input.GetKey(KeyCode.E);
			if(agent.isPlayer && IsHovering && interactionKeyPressed) 
			{
				StartInteraction();
				yield return new WaitForSeconds(1);
				StopInteraction();
			}
			else yield return null;
		}
	}

	void StartInteraction()
	{
		OnInteractionStart?.Invoke();
		animator.Play("Bite");
	}

	void StopInteraction()
	{
		OnInteractionStop?.Invoke();
	}

	public void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Interactive>(out var interactive))
		{
			HandleTrigger(true, interactive);
		}
	}

	public void OnTriggerExit2D(Collider2D collision)
	{
		if(collision.TryGetComponent<Interactive>(out var interactive))
		{
			HandleTrigger(false, interactive);
		}
	}

	void HandleTrigger(bool entering, Interactive interactive)
	{
		if (entering)
		{
			interactives.Add(interactive);
		}
		else
		{
			interactive.HoverStop();
			interactives.Remove(interactive);
		}

		if (!IsHovering) return;

		ClosestInteractive = interactives.OrderBy(x => (transform.parent.position - x.transform.position).sqrMagnitude).First();
		var otherInteractives = interactives.Where(x => x != ClosestInteractive).ToList();

		if (ClosestInteractive) ClosestInteractive.HoverStart();
		otherInteractives.ForEach(x => x.HoverStop());
	}
}
