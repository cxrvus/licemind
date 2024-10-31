public class Artery : Interactive
{
	const int DIGESTION = 5;

	public override void Interact(Louse louse)
	{
		var stats = louse.stats;
		var strength = stats.Strength;
		stats.Energy += strength * 2;
		stats.Digestion += DIGESTION;
		durability.Damage(strength);
	}
}
