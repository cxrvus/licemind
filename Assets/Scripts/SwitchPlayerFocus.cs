using Cinemachine;
using UnityEngine;

public class SwitchPlayerFocus : MonoBehaviour
{
	void Awake()
	{
		LouseStats.OnSwitchPlayer += SwitchFocus;
	}

	void SwitchFocus()
	{
		var playerTransform = LouseStats.PlayerStats.transform;
		GetComponent<CinemachineVirtualCamera>().Follow = playerTransform;
	}
}
