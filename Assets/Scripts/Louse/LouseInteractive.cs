public class LouseInteractive : Interactive
{
	public override void Interact(Louse other)
	{
		other.isPlayer = false;
		GetComponent<Louse>().isPlayer = true;
	}
}
