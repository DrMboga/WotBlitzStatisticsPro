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

        ///<summary>
        /// Tank id, which kills max frags per battle
        ///</summary>
        public long? MaxFragsTankId { get; }

        ///<summary>
        /// Tank Id which created max experience per battle
        ///</summary>
        public long? MaxXpTankId { get; }

        /// <summary>
        /// Average tier
        /// </summary>
        public double? AvgTier { get; }
    }

}