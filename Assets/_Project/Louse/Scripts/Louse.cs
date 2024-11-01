using System.Collections.Generic;
using UnityEngine;

public enum LouseState {
	Idle, Walking, Interacting
}

public partial class Louse : MonoBehaviour {
	static readonly List<Louse> lice = new ();
	public static int Count { get => lice.Count; }

	LouseState _state;
	public LouseState State {
		get => _state;
		private set
		{
			if (_state != value)
			{
				_state = value;
				UpdateAnimation();
			}
		}
	}

	void Awake()
	{
		animator = GetComponent<Animator>();
		antenna = transform.GetChild(0);
		rb = GetComponent<Rigidbody2D>();

		if(!(animator && antenna && rb)) throw new MissingComponentException();
		if(!baseStats) throw new MissingReferenceException();

		lice.Add(this);
	}

	void Start()
	{
		SetupStats();
		SetupPlayer();

		StartCoroutine(SetDirection());
		StartCoroutine(CheckForInteraction());
		StartCoroutine(ProcessStats());
	}

	void FixedUpdate()
	{
		Move();
	}
}
