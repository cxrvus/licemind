using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LouseState {
	Idle, Walking, Interacting
}

public class Louse : MonoBehaviour {
	static readonly List<Louse> lice = new ();
	public static int Count { get => lice.Count; }

	public LouseState state = LouseState.Idle;
	public GameObject defecationObject;
	public GameObject corpseObject;
	public LouseBaseStats baseStats;
	public LouseStats stats;

	void Awake()
	{
		animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody2D>();

		if(!(animator && rb)) throw new MissingComponentException();
		if(!baseStats) throw new MissingReferenceException();

		stats = new LouseStats(this);
	}

	void Start()
	{
		if (!Player) IsPlayer = true;
		lice.Add(this);

		StartCoroutine(ProcessStats());
	}

	#region Stats
	public static event Action OnGameOver;

	IEnumerator ProcessStats()
	{
		for(;;)
		{
			stats.ProcessStats();
			if (stats.Energy == 0 || stats.Age >= stats.AgeCap) Die();
			if (stats.Digestion >= stats.DigestionCap) Defecate();
			yield return new WaitForSeconds(stats.Interval);
		}
	}

	void Defecate()
	{
		SpawnAttractor(defecationObject);
		stats.Digestion = 0;
	}

	void SpawnAttractor(GameObject prefab)
	{
		var gameObject = Instantiate(prefab);
		var position = new Vector3(transform.position.x, transform.position.y, -2); // idea: parameterize Z using SO
		gameObject.transform.position = position;
	}

	void Die()
	{
		lice.Remove(this);
		SpawnAttractor(corpseObject);
		if (Count == 0) OnGameOver?.Invoke();
		else lice[Count].IsPlayer = true;
		Destroy(gameObject);
	}
	#endregion

	#region Player
	public static Louse Player { get; private set; }
	bool _isPlayer;
	public bool IsPlayer 
	{
		set
		{
			if (!value) throw new ArgumentOutOfRangeException("Can only set IsPlayer to true");
			if (Player) Player._isPlayer = false;
			_isPlayer = true;
			Player = this;
			OnSwitchPlayer?.Invoke();
		}
		get => _isPlayer;
	}
	public static event Action OnSwitchPlayer;
	#endregion

	#region Animation
	Animator animator;
	public void Play(string anim, int layer = -1) => animator.Play(anim, layer);
	// todo: automatically set animation based on state
	#endregion

	#region Movement
	Rigidbody2D rb;
	Vector2 direction;
	public bool IsMoving { get => direction.sqrMagnitude > 0; }

	void FixedUpdate() { Move(); }

	void Move()
	{
		if (state == LouseState.Interacting) return;

		if (IsPlayer) PlayerMove();
		// todo: else NpcMove();

		rb.linearVelocity = LouseStats.SPEED_FACTOR * baseStats.speed * Time.deltaTime * direction;
		if(IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
			Play("Walk");
		}
		else Play("Idle");
	}

	void PlayerMove() => direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
	#endregion
}
