using System.Collections;
using UnityEngine;

public partial class Louse
{
	Rigidbody2D rb;
	Vector2 direction;
	bool IsMoving { get => direction.sqrMagnitude > 0; }
	const float SPEED_FACTOR = 100;

	IEnumerator SetDirection()
	{
		for (;;)
		{
			if (State == LouseState.Interacting) direction = Vector3.zero;
			else
			{
				if (IsPlayer)
				{
					State = IsMoving ? LouseState.Walking : LouseState.Idle;
					direction = GetPlayerDirection();
				}
				else 
				{
					// todo: attractors
					if (State == LouseState.Idle)
					{
						State = LouseState.Walking;
						direction = Random.onUnitSphere;
					}
					else
					{

						State = LouseState.Idle;
						direction = Vector3.zero;
					}
					yield return new WaitForSeconds(Stats.WalkInterval);
				}
			}
			
			yield return null;
		}
	}

	void Move() 
	{
		rb.linearVelocity = SPEED_FACTOR * baseStats.speed * Time.deltaTime * direction;
		if(IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
