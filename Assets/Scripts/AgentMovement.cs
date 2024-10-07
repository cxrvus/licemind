using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public bool isPlayer;
	public float speed;
	const float SPEED_FACTOR = 100;

    // Update is called once per frame
	void FixedUpdate()
	{
		Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		rb.velocity = speed * SPEED_FACTOR * Time.deltaTime * direction;

		if (direction.magnitude > 0)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
}
