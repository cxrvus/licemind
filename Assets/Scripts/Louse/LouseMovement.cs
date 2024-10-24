using UnityEngine;

public class LouseMovement : MonoBehaviour
{
	public bool IsMoving { get; private set; }

	const float SPEED_FACTOR = 100;

	LouseAnimator animator;
	Interactor interactor;
	PlayerMovement playerMovement;
	Rigidbody2D rb;
	LouseStats stats;


	void Awake()
	{
		stats = GetComponent<LouseStats>();
		animator = GetComponent<LouseAnimator>();
		interactor = GetComponentInChildren<Interactor>();
		playerMovement = GetComponent<PlayerMovement>();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		var frozen = interactor.IsInteracting;
		var direction = frozen ? Vector3.zero : GetDirection();
		IsMoving = !frozen && direction.sqrMagnitude > 0;
		rb.linearVelocity = stats.Speed * SPEED_FACTOR * Time.deltaTime * direction;

		if(IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		if(!frozen) animator.Movement(IsMoving);
	}

	Vector3 GetDirection()
	{
		// todo: add NpcMovement
		if (stats.IsPlayer) return playerMovement.GetDirection();
		else return Vector3.zero;
	}
}
