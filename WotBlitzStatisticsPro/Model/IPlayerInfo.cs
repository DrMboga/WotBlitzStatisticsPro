namespace WotBlitzStatisticsPro.Model
{
    public interface IPlayerInfo
    {
        ///<summary>
        /// Player accountId
        ///</summary>
        public long AccountId { get; }

        ///<summary>
        /// Player nick
        ///</summary>
        public string Nickname { get; }

        ///<summary>
        /// Account creation date
        ///</summary>
        public DateTimeOffset CreatedAt { get; } 

        /// <summary>
        /// Player's battles count
        /// </summary>
        public long Battles { get; }

        /// <summary>
        /// Last battle time
        /// </summary>
        public DateTimeOffset LastBattle { get; }

        ///<summary>
        /// Tank id, which kills max frags per battle
        ///</summary>
        public long? MaxFragsTankId { get; }

        ///<summary>
        /// Tank Id which created max experience per battle
        ///</summary>
        public long? MaxXpTankId { get; }

        /// <summary>
        /// Clan tag. Null if player doesn't have clan membership
        /// </summary>
        public string? ClanTag { get; }

        /// <summary>
        /// Win rate from 0 to 100
        /// </summary>
        public int WinRate { get; }

        /// <summary>
        /// Average tier
        /// </summary>
        public double? AvgTier { get; }
    }
}