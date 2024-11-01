using System.Collections;
using UnityEngine;

public partial class Louse
{
	Transform antenna;
	Interactive target;

	bool CanInteract { get => target && target.CanInteract(this); }
	bool ShouldInteract { get => CanInteract && (!IsPlayer || Input.GetKey(KeyCode.E)); }

	IEnumerator CheckForInteraction()
	{
		for(;;)
		{
			var rayDirection = (antenna.rotation * Vector2.up).normalized;
			var hitCollider = Physics2D.Raycast(antenna.position, rayDirection, 0.5f).collider;
			var newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

			if (target && target != newTarget) target.HidePrompt();
			target = newTarget;
			if (IsPlayer && CanInteract) target.ShowPrompt();

			if(ShouldInteract)
			{
				State = LouseState.Interacting;

				target.HidePrompt();
				target.Interact(this);

				yield return new WaitForSeconds(target.stats.duration);

				State = LouseState.Idle;
			}

			yield return null;
		}
	}
}
