using System;
using UnityEngine;

public class LouseStats : MonoBehaviour
{
	public static LouseStats PlayerStats { get; private set; }
	bool _isPlayer;
	public bool IsPlayer { get { return _isPlayer; }
	set {
			if(value)
			{
				if (PlayerStats) PlayerStats._isPlayer = false;
				PlayerStats = this;
				_isPlayer = true;
			}
			else throw new ArgumentOutOfRangeException("Can only set IsPlayer to true");
		}
	}

	float _energy;
	public float Energy { get { return _energy; } set { _energy = Math.Clamp(value, 0, MaxEnergy); } }
	public float MaxEnergy { get; private set; }

	public float Strength { get; private set; }


	void Awake()
	{
		if (!PlayerStats) IsPlayer = true;

		// todo: spawn interactor instead of requiring
		var animator = GetComponent<LouseAnimator>();
		var interactor = GetComponentInChildren<Interactor>();
		var movement = GetComponent<LouseMovement>();

		if(!(animator && interactor && movement)) throw new MissingComponentException();
	}
}
