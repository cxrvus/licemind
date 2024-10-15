using UnityEngine;

public class AgentMovement : MonoBehaviour
{
	const float BASE_SPEED = 100;
	public Vector3 direction;
	public bool IsMoving { get { return direction.sqrMagnitude > 0; } }
	bool movementFrozen;

	Agent agent;
	Animator animator;

	void Start()
	{
		agent = GetComponent<Agent>();
		animator = GetComponent<Animator>();

		var interactor = GetComponentInChildren<Interactor>();

		if(!agent || !animator || !interactor) { throw new MissingComponentException(); }

		interactor.OnInteractionStart += Freeze;
		interactor.OnInteractionStop += Unfreeze;
	}

	void FixedUpdate()
	{
		if (movementFrozen) return;

		direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = agent.speed * BASE_SPEED * Time.deltaTime * direction;

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

	void Freeze() => movementFrozen = true;
	void Unfreeze() => movementFrozen = false;
}
