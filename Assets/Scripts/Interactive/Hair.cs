public class Hair : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = new Durability(gameObject, HairBaseStats.durabilityCap, 0.8f);
	}

	public override void Interact(LouseStats interactor)
	{
		var strength = interactor.Strength;
		interactor.Energy -= strength;
		durability.Damage(strength);
	}
}
