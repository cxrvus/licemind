using System.Collections.Generic;
using UnityEngine;

// idea: implement bit flags
public enum LState { Idle, Walking, Interacting }

public partial class Louse : MonoBehaviour {
	public AttractorBank attractors;

	static readonly List<Louse> lice = new ();
	public static int Count { get => lice.Count; }
	static int _idIncrementor;
	public int Id { get; private set; }

	void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		if(!(animator && rb)) throw new MissingComponentException();
		if(!baseStats) throw new MissingReferenceException();

		Id = _idIncrementor;
		_idIncrementor++;

		lice.Add(this);
	}

	void Start()
	{
		SetupStats();
		SetupAI();
		SetupPlayer();

		// todo: implement using timers
		StartCoroutine(ProcessStats());
	}

	void Update() => Tick();

	void FixedUpdate() => ApplyDirection();
}
