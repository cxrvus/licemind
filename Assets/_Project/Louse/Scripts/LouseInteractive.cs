public class LouseInteractive : Interactive
{
	public override bool CanInteract(Louse other) => other.IsPlayer;
	public override void Interact(Louse _) => GetComponent<Louse>().IsPlayer = true;
}
