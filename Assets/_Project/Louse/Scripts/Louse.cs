using System.Collections.Generic;
using UnityEngine;

public enum LouseState { Idle, Walking, Interacting }

public partial class Louse : MonoBehaviour {
	static readonly List<Louse> lice = new ();
	public static int Count { get => lice.Count; }
	static int _idIncrementor;
	public int Id { get; private set; }

	LouseAI ai;

	void Awake()
	{
		animator = GetComponent<Animator>();
		antenna = transform.GetChild(0);
		rb = GetComponent<Rigidbody2D>();

		if(!(animator && antenna && rb)) throw new MissingComponentException();
		if(!baseStats) throw new MissingReferenceException();

		Id = _idIncrementor;
		_idIncrementor++;

		lice.Add(this);
	}

	void Start()
	{
		SetupStats();
		ai = new LouseAI(this);
		SetupPlayer();

		StartCoroutine(CheckForInteraction());
		StartCoroutine(ProcessStats());
	}

	void Update()
	{
		if (IsPlayer) PlayerMovement();
		else ai.Tick();
	}

	void FixedUpdate() => Move();
}
