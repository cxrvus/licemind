using UnityEngine;

public class Artery : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = GetComponent<Durability>();
		durability.Setup(HairBaseStats.durabilityCap);
		Debug.Log(durability.Value);
	}

	public override void Interact(LouseStats interactor)
	{
		Debug.Log(durability.Value);
		var strength = interactor.Strength;
		interactor.Energy += strength * 2;
		durability.Damage(strength);
	}
}
