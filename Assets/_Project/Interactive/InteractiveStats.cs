using UnityEngine;

[CreateAssetMenu(fileName = "InteractiveStats", menuName = "Interactive/Stats")]
public class InteractiveStats : ScriptableObject
{
	public AnimationClip louseAnimation;
	public GameObject promptPrefab;

	public float duration;
	public int durability;
	[Range(0f, 1f)]
	public float minTransparency;
	public int effort;
}
