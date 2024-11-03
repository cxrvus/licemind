using UnityEngine;

[CreateAssetMenu(fileName = "AttractorStats", menuName = "Louse/Attractor")]
public class AttractorStats : ScriptableObject
{
	public Sprite sprite;
	public float attraction;
	public float maxRadius;
	public float minRadius;
	public float decayRate;

	public GameObject SpawnAt(Transform parent)
	{
		var instance = new GameObject();
		var position = new Vector3(parent.position.x, parent.position.y, Layers.ATTRACTOR);
		instance.transform.position = position;

		var spriteRenderer = instance.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;

		return instance;
	}
}
