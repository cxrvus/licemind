public class Artery : Interactive
{
	const int DIGESTION = 5;

	protected override void OnInteract(Louse louse)
	{
		louse.stats.Digestion += DIGESTION;
		durability.Damage();
	}
}
