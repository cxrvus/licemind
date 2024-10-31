using System.Collections;
using UnityEngine;

public class LouseInteractive : Interactive
{
	public override bool CanInteract(Louse other) => other.IsPlayer;
	public override void Interact(Louse _) => StartCoroutine(SwitchPlayer());
	
	IEnumerator SwitchPlayer()
	{
		yield return new WaitForSeconds(1);
		GetComponent<Louse>().IsPlayer = true;
	}
}
