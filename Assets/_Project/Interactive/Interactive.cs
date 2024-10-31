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

	public void ShowPrompt() { if (prompt) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public abstract void Interact(Louse louse);
	public virtual bool CanInteract(Louse louse) => true;
}
