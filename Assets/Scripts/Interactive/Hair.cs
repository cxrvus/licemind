public class Hair : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = GetComponent<Durability>();
		durability.Setup(HairBaseStats.durabilityCap);
	}

	public override void Interact(LouseStats interactor)
	{
		var strength = interactor.Strength;
		interactor.Energy -= strength;
		durability.Damage(strength);
	}
}
