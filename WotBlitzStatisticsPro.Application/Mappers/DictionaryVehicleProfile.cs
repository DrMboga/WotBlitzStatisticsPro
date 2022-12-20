namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class DictionaryVehicleProfile
    {
        public static List<DictionaryVehicle>? ToDbStructure(
            this List<WotEncyclopediaVehiclesResponse>? vehicles,
            TankTreeRowMap[]? tankTreeHelper)
        {
            if(vehicles == null)
            {
                return null;
            }
            var result = new List<DictionaryVehicle>();

            foreach (var vehicle in vehicles.OrderBy(v => v.Nation).OrderBy(v => v.Tier))
            {
                var dictionaryVehicle = new DictionaryVehicle {
                    TankId = vehicle.TankId ?? 0,
                    Tier = Convert.ToInt16(vehicle.Tier ?? 0),
                    Type = vehicle.Type ?? string.Empty,
                    Nation = vehicle.Nation ?? string.Empty,
                    IsPremium = vehicle.IsPremium,
                    PriceGold = vehicle.Cost != null && vehicle.Cost.ContainsKey("price_gold") ? vehicle.Cost["price_gold"] : 0,
                    PriceCredits = vehicle.Cost != null && vehicle.Cost.ContainsKey("price_credit") ? vehicle.Cost["price_credit"] : 0,
                    Image = vehicle.Images != null && vehicle.Images.ContainsKey("normal") ? vehicle.Images["normal"] : string.Empty,
                    PreviewImage = vehicle.Images != null && vehicle.Images.ContainsKey("preview") ? vehicle.Images["preview"] : string.Empty,
                    Name = vehicle.Name ?? string.Empty,
                    Description = vehicle.Description ?? string.Empty,
                    VehicleModulesRelation = new () // TODO: Get from WotEncyclopediaVehiclesModulesTree
                };
                if(tankTreeHelper != null)
                {
                    dictionaryVehicle.CurrentTankTreeRow = GetCurrentTankTreeRow(
                        dictionaryVehicle.TankId, 
                        tankTreeHelper, 
                        dictionaryVehicle,
                        result);
                    if(vehicle.NextTanks != null && vehicle.NextTanks.Count > 0)
                    {
                        dictionaryVehicle.NextVehicles = new ();
                        foreach (var nextTank in vehicle.NextTanks)
                        {
                            var nextVehicle = FindNextVehicle(nextTank, tankTreeHelper, dictionaryVehicle);
                            if(nextVehicle != null)
                            {
                                dictionaryVehicle.NextVehicles.Add(nextVehicle);
                            }
                        }
                    }
                }
                
                result.Add(dictionaryVehicle);
            }

            return result;
        }
    
        private static int GetCurrentTankTreeRow(
            long tankId,
            TankTreeRowMap[] tankTreeHelper,
            DictionaryVehicle dictionaryVehicle,
            List<DictionaryVehicle> parsedTanksList)
        {
            // If vehicle id is not in the tankTreeHelper, then take row from previous tank
            var tankFromTreeHelper = tankTreeHelper.FirstOrDefault(t => t.TankId == tankId);
            if(tankFromTreeHelper != null)
            {
                return tankFromTreeHelper.Row;
            }

            var previousTierTanks = parsedTanksList
                                    .Where(r => r.Nation == dictionaryVehicle.Nation && r.Tier == dictionaryVehicle.Tier - 1)
                                    .ToList();
            if(previousTierTanks.Count > 0)
            {
                var previousTankRow = previousTierTanks
                        .FirstOrDefault(pt => pt.NextVehicles != null && pt.NextVehicles.Any(n => n.NextTankId == dictionaryVehicle.TankId))
                        ?.NextVehicles
                            ?.Where(v => v.NextTankId == dictionaryVehicle.TankId)
                            .Select(v => v.TreeRowIndex)
                            .FirstOrDefault();
                if(previousTankRow != null)
                {
                    return previousTankRow.Value;
                }
            }
            return 0;
        }

        private static DictionaryNextVehicle? FindNextVehicle(
            KeyValuePair<string, long> nextTank,
            TankTreeRowMap[] tankTreeHelper,
            DictionaryVehicle dictionaryVehicle
        )
        {
            var nextTankId = long.Parse(nextTank.Key);
            var tankFromTreeHelper = tankTreeHelper.FirstOrDefault(t => t.TankId == nextTankId);
            return new DictionaryNextVehicle {
                TankId = dictionaryVehicle.TankId,
                Tank = dictionaryVehicle,
                NextTankId = nextTankId,
                PriceXP = nextTank.Value,
                TreeRowIndex = tankFromTreeHelper != null ? tankFromTreeHelper.Row : dictionaryVehicle.CurrentTankTreeRow
            };
        }
    }
}