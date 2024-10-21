using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
	public AnimationClip agentAnimation;
	public GameObject promptPrefab;
	InteractivePrompt prompt;

	void Start()
	{
		if (promptPrefab) prompt = Instantiate(promptPrefab, transform).GetComponent<InteractivePrompt>();
	}

	public void ShowPrompt(Agent agent) { if (prompt && agent.IsPlayer) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public void Interact(Agent agent)
	{
		
		CustomInteract(agent);
	}

	public abstract void CustomInteract(Agent agent);
}
