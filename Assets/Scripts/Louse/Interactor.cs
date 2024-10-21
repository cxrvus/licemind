using System.Collections;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public bool IsInteracting { get; private set; }

	Louse louse;
	LouseAnimator animator;

	void Awake()
	{
		louse = GetComponentInParent<Louse>();
		animator = GetComponentInParent<LouseAnimator>();

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
				if (newTarget) newTarget.ShowPrompt(louse);
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
		IsInteracting = true;
		animator.Interact(target);
		target.HidePrompt();
		target.Interact(louse);
	}

	void StopInteraction(Interactive target)
	{
		IsInteracting = false;
		target.ShowPrompt(louse);
	}
}
