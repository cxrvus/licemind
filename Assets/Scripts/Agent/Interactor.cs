using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Agent agent;

	public Interactive target;
	public Interactive previousTarget;

	void Start()
	{
		agent = GetComponentInParent<Agent>();
		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		for(;;)
		{
			bool interactionKeyPressed = Input.GetKey(KeyCode.E);
			if(interactionKeyPressed && target) 
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
		if (agent.isPlayer) target.ShowPrompt(false);
		target.Interact(agent);
	}

	void StopInteraction()
	{
		if (agent.isPlayer) target.ShowPrompt(true);
		agent.isInteracting = false;
	}

	void FixedUpdate()
	{
		if (agent.isInteracting) return;

		Vector2 direction = transform.rotation * Vector2.up;
		direction.Normalize();
		var hitCollider = Physics2D.Raycast(transform.position, direction, 0.5f).collider;
		var newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

		if (agent.isPlayer && newTarget != target)
		{
			if (target) target.ShowPrompt(false);
			if (newTarget) newTarget.ShowPrompt(true);
		}

		target = newTarget;
	}
}
