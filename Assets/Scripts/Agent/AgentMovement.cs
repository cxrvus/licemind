using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	public Vector3 direction;
	public bool IsMoving { get { return direction.sqrMagnitude > 0; } }

	Agent agent;

	void Start()
	{
		agent = GetComponent<Agent>();
	}

	void FixedUpdate()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		if (agent.isInteracting)
		{
			rb.velocity = Vector2.zero;
			return;
		}

		direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		rb.velocity = agent.speed * Agent.BASE_SPEED * Time.deltaTime * direction;

		if (IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			agent.PlayAnimation("Walk");
		}
		else
		{
			agent.PlayAnimation("Idle");
		}
	}
}
