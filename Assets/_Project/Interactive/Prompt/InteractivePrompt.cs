using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractivePrompt : MonoBehaviour
{
	// todo: create sprite fields for an *enabled* and *disabled* prompt & method ShowDisabled & use in Antenna if Effort > Energy
	// todo: create DisabledPrompt sprite
	SpriteRenderer spriteRenderer;

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
    }

	// idea: add cost indicator to prompt
	// idea: add action name indicator to prompt
	public void Show(bool visibility) => spriteRenderer.enabled = visibility;
}
