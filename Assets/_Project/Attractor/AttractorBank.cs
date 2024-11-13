using UnityEngine;

[CreateAssetMenu(fileName = "AttractorBank", menuName = "Louse/AttractorBank")]
public class AttractorBank : ScriptableObject
{
	public AttractorStats pheromone;
	public AttractorStats defecation;
	public AttractorStats corpse;
	public GameObject prefab;
}
