using System.Collections;

public class Artery : Interactive
{
	const int DIGESTION = 5;

	protected override IEnumerator Interaction(Louse louse) 
	{
		louse.Stats.Digestion += DIGESTION;
		yield return null;
	}
}
