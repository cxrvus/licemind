using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Clock : MonoBehaviour
{
	static GameObject clockObject;
	public static bool IsRunning { get => clockObject != null; }

	void Awake()
	{
		if (!IsRunning) clockObject = gameObject;
	}

	public static event Action OnTick;

	void Update() => OnTick?.Invoke();
}
