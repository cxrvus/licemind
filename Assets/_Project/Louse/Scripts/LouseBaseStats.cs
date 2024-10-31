using UnityEngine;

[CreateAssetMenu(fileName = "LouseBaseStats", menuName = "Louse/BaseStats")]
public class LouseBaseStats : ScriptableObject
{
	public int updateInterval;

	public int energyCap;
	public int metabolismNpcIdle;
	public int metabolismPlayerIdle;
	public int metabolismPlayerWalk;

	public int digestionCap;
	public int digestionWalk;
	public int digestionIdle;

	public int ageCap;
	public int strength;
	public int speed;
}
