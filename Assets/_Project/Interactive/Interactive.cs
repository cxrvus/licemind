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
			// idea: make prompt a child to Interactive again, instantiating it on EditorTime instead of RunTime
			var prefab = stats.promptPrefab;
			if (prefab) prompt = Instantiate(prefab, transform).GetComponent<InteractivePrompt>();

			var durabilityValue = stats.durability;
			if (durabilityValue > 0) durability = new Durability(gameObject, durabilityValue, stats.minTransparency);
		}
	}

	public void ShowPrompt() { if (prompt) prompt.Show(true); }
	public void HidePrompt() { if (prompt) prompt.Show(false); }

	public void Interact(Louse louse)
	{
		HidePrompt();
		durability?.Damage();
		OnInteract(louse);
	}

	public virtual bool CanInteract(Louse louse) => true;
	protected virtual void OnInteract(Louse louse) {}
}
