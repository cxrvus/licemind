using System.Collections;

public class Artery : Interactive
{
	const int DIGESTION = 5;
	protected override void OnInteract(Louse louse) => louse.Stats.Digestion += DIGESTION;
}
