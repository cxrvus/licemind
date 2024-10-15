using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractivePrompt : MonoBehaviour
{
	SpriteRenderer sprite;

	public void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
    }

	public void Show(bool visibility) => sprite.enabled = visibility;
}
