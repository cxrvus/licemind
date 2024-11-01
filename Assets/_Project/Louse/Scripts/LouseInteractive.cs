using System.Collections;
using UnityEngine;

public class LouseInteractive : Interactive
{
	public override bool CanInteract(Louse other) => other.IsPlayer;
	protected override void OnInteract(Louse _) => StartCoroutine(SwitchPlayer());
	
	IEnumerator SwitchPlayer()
	{
		yield return new WaitForSeconds(stats.duration);
		GetComponent<Louse>().IsPlayer = true;
	}
}
