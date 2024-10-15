using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public abstract class Interactive : MonoBehaviour
{
	SpriteRenderer promptSprite;

	void Start()
	{
		promptSprite = GetComponent<SpriteRenderer>();
    }

	public void ShowPrompt() => promptSprite.enabled = true;
	public void HidePrompt() => promptSprite.enabled = false;

	public abstract void Interact();
}
