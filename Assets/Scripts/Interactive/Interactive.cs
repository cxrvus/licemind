using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
	public AnimationClip agentAnimation;
	public GameObject promptPrefab;
	InteractivePrompt prompt;

	public void Start()
	{
		prompt = Instantiate(promptPrefab, transform).GetComponent<InteractivePrompt>();
	}

	public void ShowPrompt(bool visibility) => prompt.Show(visibility);

	public void Interact(Agent agent)
	{
		if (agentAnimation) agent.PlayAnimation(agentAnimation.name);
		OnInteract();
	}

	protected abstract void OnInteract();
}
