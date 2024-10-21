using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
	public AnimationClip louseAnimation;
	public GameObject promptPrefab;
	InteractivePrompt prompt;

	void Start()
	{
		if (promptPrefab) prompt = Instantiate(promptPrefab, transform).GetComponent<InteractivePrompt>();
	}

	public void ShowPrompt(Louse louse) { if (prompt && louse.isPlayer) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public abstract void Interact(Louse louse);
}
