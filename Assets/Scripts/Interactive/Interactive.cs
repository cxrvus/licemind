using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
	void Start()
	{
		var noCollider = !TryGetComponent<Collider2D>(out _);

		if (noCollider) { 
			Debug.LogError("Interactives require the following components: Collider2D");
			Destroy(gameObject);
			return;
		}
    }

	public void InteractionHover(bool hover) {
		Debug.Log($"hovering: {hover}");
	}
}
