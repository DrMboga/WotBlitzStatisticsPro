namespace WotBlitzStatisticsPro.Model
{
    public interface ITankInfo
    {
        ///<summary>
        /// Player account identifier
        ///</summary>
        public long AccountId { get; }

        ///<summary>
        /// Tank identifier
        ///</summary>
        public long TankId { get; }

        /// <summary>
        /// Information is tank in garage or not. Information is not provided without Auth token
        /// </summary>
        public bool? InGarage { get; }

        ///<summary>
        /// Mark of Mastery
        ///</summary>
        public MarkOfMastery MarkOfMastery { get; }

        /// <summary>
        /// Total time in battle until tank killed
        /// </summary>
        public int BattleLifeTimeInSeconds { get; }

        /// <summary>
        /// Tank name
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// Tank nation
        /// </summary>
        public string? TankNation { get; }

        /// <summary>
        /// Tank tier
        /// </summary>
        public int Tier { get; }

        /// <summary>
        /// Tank type
        /// </summary>
        public string? TankType { get; }

        /// <summary>
        /// Is it premium tank or not
        /// </summary>
        public bool IsPremium { get; }

        /// <summary>
        /// Vehicle preview image
        /// </summary>
        public string? PreviewImage { get; }

        /// <summary>
        /// Vehicle normal image
        /// </summary>
        public string? NormalImage { get; }         
    }
}