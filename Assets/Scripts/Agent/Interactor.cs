using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Agent agent;

	public Interactive nearestTarget;
	readonly List<Interactive> targets = new ();
	bool IsTargeting { get { return targets.Count > 0; } }

	public void Start()
	{
		agent = GetComponentInParent<Agent>();
		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		for(;;)
		{
			bool interactionKeyPressed = Input.GetKey(KeyCode.E);
			if(IsTargeting && interactionKeyPressed) 
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
		agent.isInteracting = true;
		if (agent.isPlayer) nearestTarget.HidePrompt();
		nearestTarget.Interact(agent);
	}

	void StopInteraction()
	{
		if (agent.isPlayer) nearestTarget.ShowPrompt();
		agent.isInteracting = false;
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
		if (agent.isInteracting) return;

		if (entering)
		{
			targets.Add(interactive);
		}
		else
		{
			targets.Remove(interactive);
			if (agent.isPlayer) interactive.HidePrompt();
		}

		if (!IsTargeting) return;

		nearestTarget = targets.OrderBy(x => (transform.parent.position - x.transform.position).sqrMagnitude).First();
		var otherInteractives = targets.Where(x => x != nearestTarget).ToList();

		if (agent.isPlayer)
		{
			if (nearestTarget) nearestTarget.ShowPrompt();
			otherInteractives.ForEach(x => x.HidePrompt());
		}
	}
}
