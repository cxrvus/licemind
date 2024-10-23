using System;
using TMPro;
using UnityEngine;

public class PlayerStatsDisplay : MonoBehaviour
{
	public GameObject energyDisplay;

	void Awake()
	{
		LouseStats.OnUpdateStats += UpdateStats;
	}

	void UpdateStats()
	{
		var player = LouseStats.PlayerStats;
		energyDisplay.GetComponent<TMP_Text>().text = StatsText(player.Energy, player.MaxEnergy, 2);
	}

	string StatsText(IFormattable current, IFormattable max, int width)
	{
		var currentString = current.ToString().PadLeft(width, '0');
		var maxString = max.ToString().PadLeft(width, '0');
		return $"{currentString} / {maxString}";
	}
}
