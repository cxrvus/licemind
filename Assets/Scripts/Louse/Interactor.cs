using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	Louse louse;

	void Start()
	{
		louse = GetComponentInParent<Louse>();
		StartCoroutine(InteractionCheck());
	}

	IEnumerator InteractionCheck()
	{
		Interactive newTarget, target = null;
		Collider2D hitCollider;

		for(;;)
		{
			var rayDirection = (transform.rotation * Vector2.up).normalized;
			hitCollider = Physics2D.Raycast(transform.position, rayDirection, 0.5f).collider;
			newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

			if (newTarget != target)
			{
				if (target) target.HidePrompt();
				if (newTarget && louse.IsPlayer) newTarget.ShowPrompt();
			}

			target = newTarget;
			bool interactionKeyPressed = Input.GetKey(KeyCode.E);

			if(interactionKeyPressed && target) 
			{
				StartInteraction(target);
				yield return new WaitForSeconds(1);
				StopInteraction(target);
			}
			else yield return null;
		}
	}

	void StartInteraction(Interactive target)
	{
		louse.state = LouseState.Interacting;
		louse.Play(target.louseAnimation.name);
		target.HidePrompt();
		target.Interact(louse);
	}

	void StopInteraction(Interactive target)
	{
		louse.state = LouseState.Idle;
		if (louse.IsPlayer) target.ShowPrompt();
	}}
