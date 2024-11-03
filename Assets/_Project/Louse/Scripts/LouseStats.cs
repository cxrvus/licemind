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

	// todo: louse size proportional to Age
	// todo: Speed anti-proportional to Age
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

		// SetupStats();
		Energy = EnergyCap;
		Louse.OnSwitchPlayer += UpdateStats;
	}

	public void Advance()
	{
		// idea: instead of this being checked every *Interval*, make it fill up *buckets* and process stats on overflow
		var depletion = louse.IsPlayer ? louse.IsMoving ? @base.playerMetabolismWalk : @base.playerMetabolismIdle : @base.npcMetabolismIdle;
		Energy -= depletion;
		Digestion += louse.IsMoving || louse.IsInteracting ? @base.digestionWork : @base.digestionIdle;
		Age++;
	}
}
