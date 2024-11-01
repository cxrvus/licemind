public class Hair : Interactive
{
	protected override void OnInteract(Louse _) => durability.Damage();
}
