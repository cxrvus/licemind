using System;
using System.Collections;
using UnityEngine;

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
		if (IsInteracting)
		{
			IsMoving = false;
			Animate(LouseState.Interacting);
			return;
		}

		Direction = GetPlayerDirection();
		if (IsMoving) Animate(LouseState.Walking);
		else Animate(LouseState.Idle);
	}

	Vector2 GetPlayerDirection() => new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	void Move() 
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
