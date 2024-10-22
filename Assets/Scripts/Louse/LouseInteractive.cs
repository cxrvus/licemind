public class LouseInteractive : Interactive
{
	public override void Interact(LouseStats other)
	{
		other.isPlayer = false;
		GetComponent<LouseStats>().isPlayer = true;
	}
}
