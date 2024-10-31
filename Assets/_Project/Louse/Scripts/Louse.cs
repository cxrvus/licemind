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

	public GameObject defecationObject;
	public GameObject corpseObject;
	public LouseBaseStats baseStats;
	public LouseStats stats;

	void Awake()
	{
		animator = GetComponent<Animator>();
		antenna = transform.GetChild(0);
		rb = GetComponent<Rigidbody2D>();

		if(!(animator && antenna && rb)) throw new MissingComponentException();
		if(!baseStats) throw new MissingReferenceException();

		stats = new LouseStats(this);
	}

	void Start()
	{
		lice.Add(this);

		if (!Player) IsPlayer = true;

		StartCoroutine(SetDirection());
		StartCoroutine(CheckForInteraction());
		StartCoroutine(ProcessStats());
	}

	void FixedUpdate()
	{
		Move();
	}

	#region Movement
	Rigidbody2D rb;
	Vector2 direction;
	bool IsMoving { get => direction.sqrMagnitude > 0; }

	IEnumerator SetDirection()
	{
		for (;;)
		{
			if (State == LouseState.Interacting) direction = Vector3.zero;
			else
			{
				if (IsPlayer)
				{
					State = IsMoving ? LouseState.Walking : LouseState.Idle;
					direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
					yield return null;
				}
				else yield return null;
				// todo: else NpcMove();
			}
			
			yield return null;
		}
	}


	void Move() 
	{
		rb.linearVelocity = LouseStats.SPEED_FACTOR * baseStats.speed * Time.deltaTime * direction;
		if(IsMoving)
		{
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
			transform.rotation = Quaternion.Euler(0, 0, angle);
		}
	}
	#endregion

	#region Stats
	public static event Action OnGameOver;

	IEnumerator ProcessStats()
	{
		for(;;)
		{
			stats.Advance();
			if (stats.Energy == 0 || stats.Age >= stats.AgeCap) Die();
			else if (stats.Digestion >= stats.DigestionCap) Defecate();
			yield return new WaitForSeconds(stats.Interval);
		}
	}

	void SpawnAttractor(GameObject prefab, bool inheritRotation = false)
	{
		var instance = Instantiate(prefab);
		var position = new Vector3(transform.position.x, transform.position.y, -2); // idea: parameterize Z using SO
		instance.transform.position = position;
		if (inheritRotation) instance.transform.rotation = transform.rotation;
	}

	void Defecate()
	{
		SpawnAttractor(defecationObject);
		stats.Digestion = 0;
	}

	void Die()
	{
		lice.Remove(this);
		SpawnAttractor(corpseObject, true);

		if (Count == 0) OnGameOver?.Invoke();
		else if (IsPlayer)
		{
			if (target) target.HidePrompt();
			lice[Count-1].IsPlayer = true;
		}

		Destroy(gameObject);
	}
	#endregion

	#region Player
	public static event Action OnSwitchPlayer;

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
	#endregion

	#region Animation
	Animator animator;
	void UpdateAnimation()
	{
		var animation = State switch
		{
			LouseState.Idle => "Idle",
			LouseState.Walking => "Walk",
			LouseState.Interacting => target.louseAnimation.name,
			_ => throw new NotImplementedException(),
		};
		
		animator.Play(animation);
	}

	#endregion

	#region Interaction
	Transform antenna;
	Interactive target;
	bool CanInteract { get => target && target.CanInteract(this); }
	bool ShouldInteract { get => CanInteract && (!IsPlayer || Input.GetKey(KeyCode.E)); }

	IEnumerator CheckForInteraction()
	{
		for(;;)
		{
			var rayDirection = (antenna.rotation * Vector2.up).normalized;
			var hitCollider = Physics2D.Raycast(antenna.position, rayDirection, 0.5f).collider;
			var newTarget = !hitCollider ? null : hitCollider.GetComponent<Interactive>();

			if (target && target != newTarget) target.HidePrompt();
			target = newTarget;
			if (IsPlayer && CanInteract) target.ShowPrompt();

			if(ShouldInteract)
			{
				State = LouseState.Interacting;
				target.HidePrompt();
				target.Interact(this);
				yield return new WaitForSeconds(1);

				if(!ShouldInteract)
				{
					State = LouseState.Idle;
					if (IsPlayer && target) target.ShowPrompt();
				}
			}

			yield return null;
		}
	}
	#endregion
}
