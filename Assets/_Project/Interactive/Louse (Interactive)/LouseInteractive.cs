using System.Collections;
using UnityEngine;

public class LouseInteractive : Interactive
{
	public override bool CanInteract(Louse other) => other.IsPlayer;
	protected override IEnumerator Interaction(Louse _) {
		// cool-down before new player can continue:
		yield return new WaitForSeconds(stats.duration);
		GetComponent<Louse>().IsPlayer = true;
	}
}
