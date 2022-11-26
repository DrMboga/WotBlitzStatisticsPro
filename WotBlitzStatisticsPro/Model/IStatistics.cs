namespace WotBlitzStatisticsPro.Model
{
    public interface IStatistics
    {
        ///<summary>
		/// Last battle time
		///</summary>
		DateTimeOffset LastBattleTime { get; }

		///<summary>
		/// Battles count
		///</summary>
		long Battles { get; }

		///<summary>
		/// Capture points
		///</summary>
		long CapturePoints { get; }

		///<summary>
		/// Total damage amount
		///</summary>
		long DamageDealt { get; }

		///<summary>
		/// Total amount of received damage
		///</summary>
		long DamageReceived { get; }

		///<summary>
		/// Dropped capture points
		///</summary>
		long DroppedCapturePoints { get; }

		///<summary>
		/// Total amount of frags
		///</summary>
		long Frags { get; }

		///<summary>
		/// Total amount of frags grater ten 8 lvl
		///</summary>
		long Frags8P { get; }

		///<summary>
		/// Total amount of hits
		///</summary>
		long Hits { get; }

		///<summary>
		/// Total amount of losses
		///</summary>
		long Losses { get; }

		///<summary>
		/// Max frags per battle
		///</summary>
		long MaxFrags { get; }

		///<summary>
		/// Max experience per battle
		///</summary>
		long MaxXp { get; }

		///<summary>
		/// Total shots count
		///</summary>
		long Shots { get; }

		///<summary>
		/// Total count of spotted vehicles
		///</summary>
		long Spotted { get; }

		///<summary>
		/// Total count of survived battles
		///</summary>
		long SurvivedBattles { get; }

		///<summary>
		/// Total count of survived and won battles
		///</summary>
		long WinAndSurvived { get; }

		///<summary>
		/// Total wins count
		///</summary>
		long Wins { get; }

		///<summary>
		/// Total amount of experience
		///</summary>
		long Xp { get; }

		/// <summary>
		/// Wn7 coefficient
		/// </summary>
		double Wn7 { get; }

		/// <summary>
		/// Player's win rate
		/// </summary>
		decimal WinRate { get; }

		/// <summary>
		/// Player's average damage
		/// </summary>
		decimal AvgDamage { get; }

		/// <summary>
		/// Player's average XP
		/// </summary>
		decimal AvgXp { get; }

		/// <summary>
		/// Damage coefficient
		/// </summary>
		decimal DamageCoefficient { get; }

		/// <summary>
		/// Rate of survival
		/// </summary>
		decimal SurvivalRate { get; }
    }
}