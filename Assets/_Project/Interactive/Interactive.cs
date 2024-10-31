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

	public void ShowPrompt() { if (prompt) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public abstract void Interact(Louse interactor);
}
