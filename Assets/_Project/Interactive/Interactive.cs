using UnityEngine;

public abstract class Interactive : MonoBehaviour
{
	public InteractiveStats stats;
	protected Durability durability;
	InteractivePrompt prompt;

	void Start()
	{
		if (stats)
		{
			var prefab = stats.promptPrefab;
			if (prefab) prompt = Instantiate(prefab, transform).GetComponent<InteractivePrompt>();

			var durabilityValue = stats.durability;
			if (durabilityValue > 0) durability = new Durability(gameObject, durabilityValue, stats.minTransparency);
		}
	}

	// idea: add cost indicator to prompt
	public void ShowPrompt() { if (prompt) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public void Interact(Louse louse)
	{
		louse.Stats.Energy -= stats.effort;
		OnInteract(louse);
	}

	protected abstract void OnInteract(Louse louse);
	public virtual bool CanInteract(Louse louse) => true;
}
