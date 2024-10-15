using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Agent agent;

	public bool isInteracting;
	public Interactive NearestTarget { get; private set; }
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
			if(agent.isPlayer && IsTargeting && interactionKeyPressed) 
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
		agent.PlayAnimation("Bite");
	}

	void StopInteraction()
	{
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
		if (entering)
		{
			targets.Add(interactive);
		}
		else
		{
			interactive.HidePrompt();
			targets.Remove(interactive);
		}

		if (!IsTargeting) return;

		NearestTarget = targets.OrderBy(x => (transform.parent.position - x.transform.position).sqrMagnitude).First();
		var otherInteractives = targets.Where(x => x != NearestTarget).ToList();

		if (NearestTarget) NearestTarget.ShowPrompt();
		otherInteractives.ForEach(x => x.HidePrompt());
	}
}
