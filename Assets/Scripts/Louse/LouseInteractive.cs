public class LouseInteractive : Interactive
{
	public override void Interact(LouseStats _)
	{
		GetComponent<LouseStats>().IsPlayer = true;
	}
}
