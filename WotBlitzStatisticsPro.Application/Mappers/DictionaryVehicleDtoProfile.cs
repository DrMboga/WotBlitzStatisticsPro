namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class DictionaryVehicleDtoProfile
    {
        public static DictionaryVehicleDto[] ToVehiclesDto(this DictionaryVehicle[] vehicles)
        {
            var result = new List<DictionaryVehicleDto>();

            if(vehicles != null && vehicles.Length > 0)            
            {
                foreach (var vehicle in vehicles)
                {
                    result.Add(vehicle.ToVehicleDto());
                }
            }
            return result.ToArray();
        }

        private static DictionaryVehicleDto ToVehicleDto(this DictionaryVehicle vehicle)
        {
            var nextTanks = vehicle.NextVehicles?
                .Select(v => new DictionaryNextVehicleDto(v.NextTankId, v.PriceXP, v.TreeRowIndex))
                .ToList();
            var modules = vehicle.VehicleModulesRelation?
                .Select(rel => new DictionaryVehicleModuleDto(
                    rel.ModuleId,
                    rel.Module?.Name ?? string.Empty,
                    rel.Module?.Type ?? string.Empty,
                    rel.Module?.PriceXp ?? 0,
                    rel.Module?.IsDefault ?? true
                ))
                .ToList();
            return new DictionaryVehicleDto(
                vehicle.TankId,
                vehicle.Tier,
                vehicle.Type,
                vehicle.Nation,
                vehicle.IsPremium,
                vehicle.PriceGold,
                vehicle.PriceCredits,
                nextTanks,
                vehicle.CurrentTankTreeRow,
                vehicle.Name,
                vehicle.Description,
                vehicle.PreviewImage,
                vehicle.Image,
                modules
            );
        }
    }
}