public class Artery : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = new Durability(gameObject, InteractiveBaseStats.durabilityCap);
	}

	public override void Interact(LouseStats interactor)
	{
		var strength = interactor.Strength;
		interactor.Energy += strength * 2;
		interactor.Digestion += InteractiveBaseStats.digestion;
		durability.Damage(strength);
	}
}
