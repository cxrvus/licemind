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

	public void ShowPrompt(bool visibility) { if (prompt) prompt.Show(visibility); }

	public void Interact(Agent agent)
	{
		if (agentAnimation) agent.PlayAnimation(agentAnimation.name);
		OnInteract(agent);
	}

	protected abstract void OnInteract(Agent agent);
}
