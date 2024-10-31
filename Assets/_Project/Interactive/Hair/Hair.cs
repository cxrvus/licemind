public class Hair : Interactive
{
	public override void Interact(Louse louse)
	{
		var stats = louse.stats;
		var strength = stats.Strength;
		stats.Energy -= strength;
		durability.Damage(strength);
	}
}
