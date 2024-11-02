using System.Collections;
using UnityEngine;

public class Interactive : MonoBehaviour
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
	// idea: add action name indicator to prompt
	public void ShowPrompt() { if (prompt) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public void Interact(Louse louse)
	{
		HidePrompt();
		durability?.Damage();
		StartCoroutine(Interaction(louse));
	}

	public virtual bool CanInteract(Louse louse) => true;

	protected virtual IEnumerator Interaction(Louse louse)
	{
		yield return null;
	}
}
