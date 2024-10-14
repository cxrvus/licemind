using System;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public void OnTriggerEnter2D(Collider2D collision) {
		if(collision.TryGetComponent<Interactive>(out var interactive))
		{
			interactive.InteractionHover(true);
		}
	}

	public void OnTriggerExit2D(Collider2D collision) {
		if(collision.TryGetComponent<Interactive>(out var interactive))
		{
			interactive.InteractionHover(false);
		}
	}
}
