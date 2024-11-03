using System.Collections;
using UnityEngine;

public partial class Louse
{
	Transform antenna;
	Interactive target;
	
	bool _isInteracting;
	public bool IsInteracting
	{
		get => _isInteracting;
		private set
		{
			_isInteracting = value;
			if (value) AnimateInteraction();
		}
	}

	IEnumerator CheckForInteraction()
	{
		for(;;)
		{
			var rayDirection = (antenna.rotation * Vector2.up).normalized;
			var hitCollider = Physics2D.Raycast(antenna.position, rayDirection, 0.5f).collider;
			var newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

			if (target && target != newTarget) target.HidePrompt();

			target = newTarget;

			if (target)
			{
				var canInteract = CanInteract(target);
				if (IsPlayer && canInteract) target.ShowPrompt();

				if (canInteract && ShouldInteract(target))
				{
					Interact(target);
					yield return new WaitForSeconds(target.stats.duration);
					IsInteracting = false;
				}
			}

			yield return null;
		}
	}

	void Interact(Interactive target)
	{
		IsInteracting = true;
		Stats.Energy -= target.stats.effort;
		target.Interact(this);
	}

	bool CanInteract(Interactive target) 
	{
		return Stats.Energy > target.stats.effort && target.CanInteract(this);
	}

	// todo: flesh out check for Npc (better than just !IsPlayer)
	bool ShouldInteract(Interactive _)
	{
		return !IsPlayer || Input.GetKey(KeyCode.E);
	}
}
