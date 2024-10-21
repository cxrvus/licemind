using System;
using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public event Action<Interactive> OnStartInteraction;
	public event Action<Interactive> OnStopInteraction;

	public Interactive previousTarget;

	Agent agent;

	void Awake()
	{
		agent = GetComponentInParent<Agent>();

		OnStartInteraction += StartInteraction;
		OnStopInteraction += StopInteraction;

		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		Interactive newTarget, target = null;
		Collider2D hitCollider;

		for(;;)
		{
			Vector2 direction = transform.rotation * Vector2.up;
			direction.Normalize();
			hitCollider = Physics2D.Raycast(transform.position, direction, 0.5f).collider;
			newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

			if (newTarget != target)
			{
				if (target) target.HidePrompt();
				if (newTarget) newTarget.ShowPrompt(agent);
			}

			target = newTarget;
			bool interactionKeyPressed = Input.GetKey(KeyCode.E);

			if(interactionKeyPressed && target) 
			{
				OnStartInteraction.Invoke(target);
				yield return new WaitForSeconds(1);
				OnStopInteraction.Invoke(target);
			}
			else yield return null;
		}
	}

	void StartInteraction(Interactive target)
	{
		target.HidePrompt();
		target.CustomInteract(agent);
	}

	void StopInteraction(Interactive target)
	{
		target.ShowPrompt(agent);
	}
}
