using Cinemachine;
using UnityEngine;

public class SwitchPlayerFocus : MonoBehaviour
{
	void Awake()
	{
		Louse.OnSwitchPlayer += SwitchFocus;
	}

	void SwitchFocus()
	{
		var playerTransform = Louse.Player.transform;
		GetComponent<CinemachineVirtualCamera>().Follow = playerTransform;
	}
}
