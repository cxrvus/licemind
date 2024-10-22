public class LouseInteractive : Interactive
{
	public override void Interact(LouseStats other)
	{
		other.BecomeNpc();
		GetComponent<LouseStats>().BecomeNpc();
	}
}
