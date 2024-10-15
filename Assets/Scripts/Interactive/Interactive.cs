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

	public void HoverStart() => sprite.enabled = true;
	public void HoverStop() => sprite.enabled = false;

	public abstract void Interact();
}
