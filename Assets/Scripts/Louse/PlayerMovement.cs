using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public Vector3 GetDirection()
	{
		return new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
	}
}
