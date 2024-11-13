using UnityEngine;

[CreateAssetMenu(fileName = "AttractorStats", menuName = "Louse/Attractor")]
public class AttractorStats : ScriptableObject
{
	public Sprite sprite;
	public Color auraColor;
	public float attraction;
	public float maxRadius;
	public float minRadius;
	public float decayRate;

}
