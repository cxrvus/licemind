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
	public int digestionWork;
	public int digestionIdle;

	public int ageCap;
	public int speed;
}
