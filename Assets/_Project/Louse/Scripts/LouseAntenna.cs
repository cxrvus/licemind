using System.Collections;
using UnityEngine;

public partial class Louse
{
	Interactive target;
	
	bool CanInteract { get => Stats.Energy > target.Stats.effort && target.CanInteract(this); }
	bool ShouldInteract { get => !IsPlayer || Input.GetKey(KeyCode.E); }

	bool InteractionCheck()
	{
		var rayDirection = (transform.rotation * Vector2.up).normalized;
		var rayPosition = transform.position + rayDirection * 0.5f;
		var rayDistance = baseStats.interactionDistance;

		var hitCollider = Physics2D.Raycast(rayPosition, rayDirection, rayDistance).collider;
		var newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

		if (target && target != newTarget) target.HidePrompt();

		target = newTarget;

		if (target)
		{
			if (IsPlayer) target.ShowPrompt();
			{
				if (CanInteract) target.ShowPrompt();
				else target.HidePrompt();
			}

			return CanInteract && ShouldInteract;
		}
		else return false;
	}
}
