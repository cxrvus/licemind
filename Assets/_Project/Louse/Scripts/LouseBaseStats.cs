using UnityEngine;

[CreateAssetMenu(fileName = "LouseBaseStats", menuName = "Louse/BaseStats")]
public class LouseBaseStats : ScriptableObject
{
	public float updateInterval;
	public float walkInterval;

	public float interactionDistance;

	public int energyCap;

	public int npcMetabolismIdle;
	public int playerMetabolismIdle;
	public int playerMetabolismWalk;

	public int digestionCap;
	public int digestionWork;
	public int digestionIdle;

	public int ageCap;
	public int speed;
}
