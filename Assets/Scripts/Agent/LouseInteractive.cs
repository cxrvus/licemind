using System;

public class LouseInteractive : Interactive
{
	public override void CustomInteract(Agent other)
	{
		other.BecomeNpc();
		GetComponent<Agent>().BecomePlayer();
	}
}
