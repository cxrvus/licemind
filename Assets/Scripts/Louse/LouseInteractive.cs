public class LouseInteractive : Interactive
{
	public override void Interact(LouseStats other)
	{
		GetComponent<LouseStats>().IsPlayer = true;
	}
}
