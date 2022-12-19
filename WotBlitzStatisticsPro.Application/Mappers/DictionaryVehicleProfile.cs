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

            // TODO: Mappings with filled NextVehicles and VehicleModulesRelation

            return result;
        }
    }
}