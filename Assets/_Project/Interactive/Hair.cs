public class Hair : Interactive
{
	Durability durability;

	void Awake()
	{
		durability = new Durability(gameObject, InteractiveBaseStats.durabilityCap, 0.8f);
	}

	public override void Interact(Louse louse)
	{
		var stats = louse.stats;
		var strength = stats.Strength;
		stats.Energy -= strength;
		durability.Damage(strength);
	}
}
