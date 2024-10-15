using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
	SpriteRenderer sprite;

	void Start()
	{
		TryGetComponent(out sprite);

		var noAnimator = !TryGetComponent<Animator>(out _);
		var noCollider = !TryGetComponent<Collider2D>(out _);
		var noSprite = !sprite;

		if (noAnimator || noCollider || noSprite) { 
			Debug.LogError("Interactives require the following components: Animator, Collider2D, SpriteRenderer");
			Destroy(gameObject);
			return;
		}
    }

	public void InteractionHover(bool hover) {
		sprite.enabled = hover;
	}

	public abstract void Interact();
}
