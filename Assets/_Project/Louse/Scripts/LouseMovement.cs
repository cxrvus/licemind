using System;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Louse
{
	const float SPEED_FACTOR = 100;
	const float PLAYER_SPEED = 2;

	Rigidbody2D rb;

	Vector2 _direction;
	public Vector2 Direction
	{
		get => _direction;
		set
		{
			_direction = value;
			if (IsMoving) _direction = _direction.normalized;
		}
	}
	public bool IsMoving
	{ 
		get => Direction.sqrMagnitude > 0;
		set {
			if (value) throw new ArgumentOutOfRangeException("Can only set IsMoving to false");
			Direction = Zero;
		}
	}
	Vector2 Zero { get => Vector2.zero; }

	void PlayerMovement()
	{
		if (State == LState.Interacting) return;

		Direction = GetPlayerDirection();
		State = IsMoving ? LState.Walking : LState.Idle;
	}

	Vector2 GetPlayerDirection() => new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	void NpcMovement()
	{
		if (walkCycle.IsFinished)
		{
			if (State == LState.Walking)
			{
				State = LState.Idle;
				Direction = Zero;
			}
			else if (State == LState.Idle) 
			{
				Direction = NpcDirection();
				State = LState.Walking;
			}
			walkCycle.Reset();
		}
	}

	Vector2 NpcDirection()
	{
		Vector2 directionSum = Random.onUnitSphere;

		for (var i = 0; i < nearbyAttractors.Count; i++)
		{
			var attractor = nearbyAttractors[i];
			Vector2 direction = transform.position - attractor.transform.position;

			if (direction.magnitude <= attractor.stats.minRadius) continue;
			else directionSum += attractor.stats.attraction * -attractor.Radius * direction.normalized;
		}

		return directionSum;
	}

	void ApplyDirection() 
	{
		var playerFactor = IsPlayer ? PLAYER_SPEED : 1;
		rb.linearVelocity = SPEED_FACTOR * playerFactor * baseStats.speed * Time.deltaTime * Direction;
		if(IsMoving)
		{
			float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
