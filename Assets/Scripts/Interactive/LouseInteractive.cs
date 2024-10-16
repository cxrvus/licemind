using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LouseInteractive : Interactive
{
	protected override void OnInteract(Agent other)
	{
		other.isPlayer = false;
		GetComponent<Agent>().isPlayer = true;
	}
}
