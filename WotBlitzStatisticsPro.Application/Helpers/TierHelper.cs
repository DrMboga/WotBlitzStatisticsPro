namespace WotBlitzStatisticsPro.Application.Helpers
{
    public static class TierHelper
    {
        public static double AvgTier(this TankInfoDto[]? tanks, long accountBattles)
        {
            double x = 0d;
            if( tanks == null || tanks.Length == 0)
            {
                return x;
            }

            foreach (var tank in tanks)
            {
                if (tank.Battles > 0)
                {
                    x += tank.Tier * tank.Battles.DoubleValue();
                }
            }
            if (accountBattles > 0)
                x /= accountBattles;
            return x;
        }
    }
}