using System;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
	public GameObject energyDisplay;
	public GameObject digestionDisplay;
	public GameObject ageDisplay;
	public GameObject countDisplay;

	void Awake()
	{
		LouseStats.OnUpdateStats += UpdateStats;
	}

	void UpdateStats()
	{
		var stats = Louse.Player.Stats;

		// idea: use a prefab and SO for stat displays
		energyDisplay.GetComponent<TMP_Text>().text = StatsText(stats.Energy, stats.EnergyCap, 2);
		digestionDisplay.GetComponent<TMP_Text>().text = StatsText(stats.Digestion, stats.DigestionCap, 2);
		ageDisplay.GetComponent<TMP_Text>().text = StatsText(stats.Age, stats.AgeCap, 2);
		countDisplay.GetComponent<TMP_Text>().text = Louse.Count.ToString().PadLeft(2, '0');
	}

	string StatsText(IFormattable current, IFormattable max, int width)
	{
		var currentString = current.ToString().PadLeft(width, '0');
		var maxString = max.ToString().PadLeft(width, '0');
		return $"{currentString} / {maxString}";
	}
}
