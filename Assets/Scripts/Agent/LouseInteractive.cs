public class LouseInteractive : Interactive
{
	public override void Interact(Agent other)
	{
		other.isPlayer = false;
		GetComponent<Agent>().isPlayer = true;
	}
}
