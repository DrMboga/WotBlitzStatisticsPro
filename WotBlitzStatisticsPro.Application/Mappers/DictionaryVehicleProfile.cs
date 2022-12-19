namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class DictionaryVehicleProfile
    {
        public static List<DictionaryVehicle>? ToDbStructure(
            this List<WotEncyclopediaVehiclesResponse>? vehicles,
            WotEncyclopediaVehicleModulesResponse vehicleModules,
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
                    VehicleModulesRelation = new ()
                };
                if(vehicle.NextTanks != null)
                {
                    // TODO: If vehicle id is not in the tankTreeHelper, then take row from previous tank
                }
                
                if(vehicle.Suspensions != null)
                {
                    foreach (var moduleId in vehicle.Suspensions)
                    {
                        var relation = new DictionaryVehicleModuleRelation 
                        {
                            ModuleId = moduleId,
                            Module = vehicleModules.Suspensions?
                                .Where(s => s.ModuleId == moduleId)
                                .Select(s => s.ToDictionaryVehicle(
                                    "suspension",
                                    dictionaryVehicle.VehicleModulesRelation))
                                .FirstOrDefault(),
                            TankId = vehicle.TankId ?? 0,
                            Tank = dictionaryVehicle
                        };
                        dictionaryVehicle.VehicleModulesRelation.Add(relation);
                    }
                }
                if(vehicle.Turrets != null)
                {
                    foreach (var moduleId in vehicle.Turrets)
                    {
                        var relation = new DictionaryVehicleModuleRelation 
                        {
                            ModuleId = moduleId,
                            Module = vehicleModules.Turrets?
                                .Where(s => s.ModuleId == moduleId)
                                .Select(s => s.ToDictionaryVehicle(
                                    "turret",
                                    dictionaryVehicle.VehicleModulesRelation))
                                .FirstOrDefault(),
                            TankId = vehicle.TankId ?? 0,
                            Tank = dictionaryVehicle
                        };
                        dictionaryVehicle.VehicleModulesRelation.Add(relation);
                    }
                }
                if(vehicle.Engines != null)
                {
                    foreach (var moduleId in vehicle.Engines)
                    {
                        var relation = new DictionaryVehicleModuleRelation 
                        {
                            ModuleId = moduleId,
                            Module = vehicleModules.Engines?
                                .Where(s => s.ModuleId == moduleId)
                                .Select(s => s.ToDictionaryVehicle(
                                    "engine",
                                    dictionaryVehicle.VehicleModulesRelation))
                                .FirstOrDefault(),
                            TankId = vehicle.TankId ?? 0,
                            Tank = dictionaryVehicle
                        };
                        dictionaryVehicle.VehicleModulesRelation.Add(relation);
                    }
                }
                if(vehicle.Guns != null)
                {
                    foreach (var moduleId in vehicle.Guns)
                    {
                        var relation = new DictionaryVehicleModuleRelation 
                        {
                            ModuleId = moduleId,
                            Module = vehicleModules.Guns?
                                .Where(s => s.ModuleId == moduleId)
                                .Select(s => s.ToDictionaryVehicle(
                                    "gun",
                                    dictionaryVehicle.VehicleModulesRelation))
                                .FirstOrDefault(),
                            TankId = vehicle.TankId ?? 0,
                            Tank = dictionaryVehicle
                        };
                        dictionaryVehicle.VehicleModulesRelation.Add(relation);
                    }
                }

                result.Add(dictionaryVehicle);
            }

            return result;
        }
    
        private static DictionaryVehicleModule ToDictionaryVehicle(
            this WotEncyclopediaVehicleModule vehicleModule, 
            string moduleType,
            List<DictionaryVehicleModuleRelation> relationsReference)
        {
            return new DictionaryVehicleModule
            {
                ModuleId = vehicleModule.ModuleId ?? 0,
                Tier = Convert.ToInt32(vehicleModule.Tier ?? 0),
                Weight = vehicleModule.Weight ?? 0,
                Nation = vehicleModule.Nation ?? string.Empty,
                Name = vehicleModule.Name ?? string.Empty,
                ModuleType = moduleType,
                VehicleModulesRelation = relationsReference
            };
        }
    }
}