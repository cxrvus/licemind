using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
	public GameObject display;

	void Awake()
	{
		LouseStats.OnGameOver += Show;
	}

	void Show()
	{
		display.SetActive(true);
	}
}
