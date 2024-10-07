using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public bool isPlayer;
	public float speed;

    // Update is called once per frame
    void Update()
    {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 direction = new Vector3(x, y, 0).normalized;
		Transform transform = gameObject.GetComponent<Transform>();
		transform.position += this.speed * Time.deltaTime * direction;
    }
}
