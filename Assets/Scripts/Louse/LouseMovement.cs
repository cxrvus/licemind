using UnityEngine;

public class LouseMovement : MonoBehaviour
{
	public float speed;
	const float BASE_SPEED = 100;

	LouseAnimator animator;
	Interactor interactor;
	PlayerMovement playerMovement;
	Rigidbody2D rb;
	LouseStats _stats;
	bool IsPlayer { get { return _stats.isPlayer; } }


	void Awake()
	{
		_stats = GetComponent<LouseStats>();
		animator = GetComponent<LouseAnimator>();
		interactor = GetComponentInChildren<Interactor>();
		playerMovement = GetComponent<PlayerMovement>();
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		var frozen = interactor.IsInteracting;
		var direction = frozen ? Vector3.zero : GetDirection();
		var isMoving = !frozen && direction.sqrMagnitude > 0;
		rb.velocity = speed * BASE_SPEED * Time.deltaTime * direction;

		if(isMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}

		if(!frozen) animator.Movement(isMoving);
	}

	Vector3 GetDirection()
	{
		// todo: add NpcMovement
		if (IsPlayer) return playerMovement.GetDirection();
		else return Vector3.zero;
	}
}
