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
		var player = LouseStats.PlayerStats;

		energyDisplay.GetComponent<TMP_Text>().text = StatsText(player.Energy, player.EnergyCap, 2);
		digestionDisplay.GetComponent<TMP_Text>().text = StatsText(player.Digestion, player.DigestionCap, 2);
		ageDisplay.GetComponent<TMP_Text>().text = StatsText(player.Age, player.AgeCap, 2);
		countDisplay.GetComponent<TMP_Text>().text = LouseStats.LouseCount.ToString().PadLeft(2, '0');
	}

	string StatsText(IFormattable current, IFormattable max, int width)
	{
		var currentString = current.ToString().PadLeft(width, '0');
		var maxString = max.ToString().PadLeft(width, '0');
		return $"{currentString} / {maxString}";
	}
}
