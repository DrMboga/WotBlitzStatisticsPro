namespace WotBlitzStatisticsPro.Application.Helpers
{
    public static class TankEquipmentHelper
    {
        private record EquipmentPrice(int Teir, int Level, int Price);
        private static EquipmentPrice[] Prices = new[] {
            new EquipmentPrice(5, 1, 80000),
            new EquipmentPrice(5, 2, 90000),
            new EquipmentPrice(5, 3, 100000),
            new EquipmentPrice(6, 1, 100000),
            new EquipmentPrice(6, 2, 120000),
            new EquipmentPrice(6, 3, 140000),
            new EquipmentPrice(7, 1, 150000),
            new EquipmentPrice(7, 2, 170000),
            new EquipmentPrice(7, 3, 190000),
            new EquipmentPrice(8, 1, 200000),
            new EquipmentPrice(8, 2, 225000),
            new EquipmentPrice(8, 3, 250000),
            new EquipmentPrice(9, 1, 250000),
            new EquipmentPrice(9, 2, 275000),
            new EquipmentPrice(9, 3, 300000),
            new EquipmentPrice(10, 1, 300000),
            new EquipmentPrice(10, 2, 350000),
            new EquipmentPrice(10, 3, 400000),
        };

        public static int CalculateEquipment(this short tier, int equipmentNumber)
        {
            Console.WriteLine($"Tier {tier}; {equipmentNumber}");
            if (tier < 5 || tier > 10 || equipmentNumber < 1 || equipmentNumber > 9)
            {
                return 0;
            }

            var prices = Prices.Where(p => p.Teir == tier).ToArray();
            int firstLevelPrice = prices.First(p => p.Level == 1).Price;
            int secondLevelPrice = prices.First(p => p.Level == 2).Price;
            int thirdLevelPrice = prices.First(p => p.Level == 3).Price;

            int columnNumber = equipmentNumber % 3;
            return equipmentNumber switch
            {
                ( > 0 ) and ( < 3 ) => firstLevelPrice * columnNumber,
                ( >= 3 ) and ( < 6 ) => firstLevelPrice * 3 + secondLevelPrice * columnNumber,
                ( >= 6 ) and ( < 9 ) => firstLevelPrice * 3 + secondLevelPrice * 3 + thirdLevelPrice * columnNumber,
                _ => firstLevelPrice * 3 + secondLevelPrice * 3 + thirdLevelPrice * 3
            };

        }
    }
}