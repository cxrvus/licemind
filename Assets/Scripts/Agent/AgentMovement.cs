using System;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	public float speed;
	const float BASE_SPEED = 100;

	bool isFrozen;
	Rigidbody2D rb;

	public event Action<bool> OnMovement;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	//TODO
	public void Freeze(bool frozen) => isFrozen = frozen;

	void Move(Vector3 direction)
	{
		if (isFrozen)
		{
			rb.velocity = Vector2.zero;
			return;
		}

		rb.velocity = speed * BASE_SPEED * Time.deltaTime * direction;

		var isMoving = direction.sqrMagnitude > 0;

		if(isMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		OnMovement.Invoke(isMoving);
	}
}
