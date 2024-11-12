using UnityEngine;

public class BloodOverlay : MonoBehaviour
{
	Durability durability;
	Louse louse;

	void Start()
	{
		louse = GetComponentInParent<Louse>();
		var interactiveStats = GetComponentInParent<LouseInteractive>().Stats;

		var minTransp = interactiveStats.minTransparency;
		var valueCap = louse.Stats.EnergyCap;

		durability = new Durability(gameObject, valueCap, minTransp);
	}

	public void UpdateTransp() => durability.Value = louse.Stats.Energy;
}
