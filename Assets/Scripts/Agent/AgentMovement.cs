using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	const float SPEED_FACTOR = 100;
	public Vector3 direction;
	public bool IsMoving { get { return direction.sqrMagnitude > 0; } }

	Agent agent;
	Animator animator;

	void Start()
	{
		agent = GetComponent<Agent>();
		animator = GetComponent<Animator>();

		if(!agent || !animator) { throw new MissingComponentException(); }
	}

	void FixedUpdate()
	{
		direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = agent.speed * SPEED_FACTOR * Time.deltaTime * direction;

		if (IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			animator.Play("Walk");
		}
		else
		{
			animator.Play("Idle");
		}
	}
}
