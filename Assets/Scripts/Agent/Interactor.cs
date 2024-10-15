using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public bool isInteracting;
	public Interactive ClosestInteractive { get; private set; }
	readonly List<Interactive> interactives = new ();

	public event Action OnInteractionStart;
	public event Action OnInteractionStop;

	public void Start()
	{
		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		for(;;)
		{
			if(Input.GetKeyDown(KeyCode.E))
			{
				OnInteractionStart?.Invoke();
				Debug.Log("start");
				yield return new WaitForSeconds(1);
				OnInteractionStop?.Invoke();
				Debug.Log("stop");
			}
			else yield return null;
		}
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
			interactive.InteractionHover(false);
			interactives.Remove(interactive);
		}

		if (interactives.Count == 0) return;

		ClosestInteractive = interactives.OrderBy(x => (transform.parent.position - x.transform.position).sqrMagnitude).First();
		var otherInteractives = interactives.Where(x => x != ClosestInteractive).ToList();

		if (ClosestInteractive) ClosestInteractive.InteractionHover(true);
		otherInteractives.ForEach(x => x.InteractionHover(false));
	}
}
