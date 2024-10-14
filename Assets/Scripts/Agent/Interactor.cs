using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor : MonoBehaviour
{

	Interactive closestInteractive;
	readonly List<Interactive> interactives = new ();

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
			interactive.InteractionHover(false);
			interactives.Remove(interactive);
		}

		if (interactives.Count == 0) return;

		closestInteractive = interactives.OrderBy(x => (transform.parent.position - x.transform.position).sqrMagnitude).First();
		var otherInteractives = interactives.Where(x => x != closestInteractive).ToList();

		if (closestInteractive) closestInteractive.InteractionHover(true);
		otherInteractives.ForEach(x => x.InteractionHover(false));
	}
}
