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

	public int Speed { get => @base.speed; }
	public int Interval { get => @base.updateInterval; }

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
		var walking = louse.State == LouseState.Walking;
		var depletion = louse.IsPlayer ? walking ? @base.metabolismPlayerWalk : @base.metabolismPlayerIdle : @base.metabolismNpcIdle;
		Energy -= depletion;
		Digestion += walking ? @base.digestionWalk : @base.digestionIdle;
		Age++;
	}
}
