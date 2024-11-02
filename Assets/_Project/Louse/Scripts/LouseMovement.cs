using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Louse
{
	const float SPEED_FACTOR = 100;
	Rigidbody2D rb;
	Vector2 direction;
	bool IsMoving { get => direction.sqrMagnitude > 0; }
	Vector3 Zero { get => Vector3.zero; }

	IEnumerator SetDirection()
	{
		for (;;)
		{
			if (State == LouseState.Interacting)
			{
				direction = Zero;
			}
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
						var randomDir = Random.onUnitSphere;
						randomDir.z = 0;
						direction = randomDir.normalized;
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
