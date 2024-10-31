using UnityEngine;

public class GameOverDisplay : MonoBehaviour
{
	public GameObject display;

	void Awake()
	{
		Louse.OnGameOver += Show;
	}

	void Show()
	{
		display.SetActive(true);
	}
}
