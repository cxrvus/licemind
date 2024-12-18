using System;

public class LouseStats
{
	int _energy;
	public int EnergyCap { get => @base.energyCap; }
	public int Energy { get => _energy; set { _energy = Math.Clamp(value, 0, EnergyCap); UpdateStats(); } }

	int _age;
	public int AgeCap { get => @base.ageCap; }
	public int Age { get => _age; private set { _age = value; UpdateStats(); } }

	int _digestion;
	public int DigestionCap { get => @base.digestionCap; }
	public int Digestion { get => _digestion; set { _digestion = Math.Clamp(value, 0, DigestionCap); UpdateStats(); } }

	// idea: louse size proportional to Age
	// idea: Speed anti-proportional to Age
	public int Speed { get => @base.speed; }
	public float UpdateInterval { get => @base.updateInterval; }
	public float WalkInterval { get => @base.walkInterval; }

	// idea: re-add Strength stat, proportional to Age

	readonly Louse louse;
	readonly LouseBaseStats @base;
	public static event Action OnUpdateStats;
	void UpdateStats() 
	{
		if (louse.IsPlayer) OnUpdateStats?.Invoke();
	}

	public LouseStats(Louse louse)
	{
		if(!louse) throw new ArgumentNullException();
		this.louse = louse;
		@base = louse.baseStats;

		Energy = EnergyCap;
		Louse.OnSwitchPlayer += UpdateStats;
	}

	public void PassiveUpdate()
	{
		// todo: use Timers
		var walking = louse.State == LState.Walking;
		var interacting = louse.State == LState.Interacting;
		var depletion = louse.IsPlayer ? walking ? @base.playerMetabolismWalk : @base.playerMetabolismIdle : @base.npcMetabolismIdle;
		Energy -= depletion;
		Digestion += walking || interacting ? @base.digestionWork : @base.digestionIdle;
		Age++;
	}
}
