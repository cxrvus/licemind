using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	public Vector3 Direction { get; private set; }
	public bool IsMoving { get { return Direction.magnitude > 0; } }

	public bool isPlayer;
	public float speed;
	const float SPEED_FACTOR = 100;

	Animator animator;

	void Start()
	{
		animator = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		Direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = speed * SPEED_FACTOR * Time.deltaTime * Direction;

		if (IsMoving)
		{
			float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			animator.Play("Walk");
		}
		else
		{
			animator.Play("Idle");
		}
	}
}
