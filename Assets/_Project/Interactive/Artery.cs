public class Artery : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = new Durability(gameObject, InteractiveBaseStats.durabilityCap);
	}

	public override void Interact(Louse louse)
	{
		var stats = louse.stats;
		var strength = stats.Strength;
		stats.Energy += strength * 2;
		stats.Digestion += InteractiveBaseStats.digestion;
		durability.Damage(strength);
	}
}
