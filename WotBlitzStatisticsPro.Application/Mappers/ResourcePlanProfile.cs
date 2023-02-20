namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class ResourcePlanProfile
    {
        public static ResourcePlanDto[]? ToResourcePlanDto(this List<ResourcePlanning>? resourcePlannings)
        {
            if (resourcePlannings == null)
            {
                return null;
            }
            return resourcePlannings.Select(r => new ResourcePlanDto{
                TankId = r.TankId,
                Order = r.Order,
                SaleCert = r.SaleCert,
                PlanningEquipment = r.PlanningEquipment,
                Bought = r.Bought
            }).ToArray();
        }
    }
}