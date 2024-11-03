using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public partial class Louse
{
	const float SPEED_FACTOR = 100;
	const float PLAYER_SPEED = 2;

	Rigidbody2D rb;

	Vector2 _direction;
	Vector2 Direction
	{
		get => _direction;
		set
		{
			_direction = value;

			if (IsInteracting) AnimateInteraction();
			else if (IsMoving)
			{
				_direction = _direction.normalized;
				AnimateWalk();
			}
			else AnimateIdle();
		}
	}
	public bool IsMoving
	{ 
		get => _direction.sqrMagnitude > 0;
		private set {
			if (value) throw new ArgumentOutOfRangeException("Can only set IsMoving to false");
			_direction = Zero;
			AnimateIdle();
		}
	}
	Vector2 Zero { get => Vector2.zero; }

	Coroutine npcMovement;

	void SetupMovement()
	{
		StartCoroutine(Movement());
		if (!IsPlayer) BecomeNpc();
	}

	IEnumerator Movement()
	{
		for (;;)
		{
			if (IsInteracting) IsMoving = false;
			else if (IsPlayer) Direction = GetPlayerDirection();
			yield return null;
		}
	}

	Vector2 GetPlayerDirection() => new (Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	IEnumerator NpcMovement()
	{
		for (;;)
		{
			Debug.Log($"{Id}");
			if (IsPlayer) yield break;
			if (IsInteracting) yield return null;

			if (IsMoving) IsMoving = false;
			else Direction = GetNpcDirection();

			yield return new WaitForSeconds(Stats.WalkInterval);
		}
	}

	Vector2 GetNpcDirection()
	{
		// todo: attractors
		var randomDir = Random.onUnitSphere;
		randomDir.z = 0; // TODO: unneeded?
		return randomDir;
	}

	void FixedUpdate() => Move();

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
