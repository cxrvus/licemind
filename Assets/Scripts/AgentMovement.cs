using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public bool isPlayer;
	public float speed;
	const float SPEED_FACTOR = 1000;

    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(x, y, 0).normalized;
		Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
		rb.velocity = this.speed * SPEED_FACTOR * Time.deltaTime * direction;
    }
}
