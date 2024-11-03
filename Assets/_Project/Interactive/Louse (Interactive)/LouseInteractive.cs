using System.Collections;
using UnityEngine;

public class LouseInteractive : Interactive
{
	Louse self;
	void Awake() => self = GetComponent<Louse>();
	public override bool CanInteract(Louse other) => other.IsPlayer && self.State != LState.Interacting;
	protected override void OnInteract(Louse _) => self.BecomePlayer();
}
