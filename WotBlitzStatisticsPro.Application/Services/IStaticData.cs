namespace WotBlitzStatisticsPro.Application.Services
{
    public interface IStaticData
    {
        Task<TankTreeRowMap[]?> GetTanksTreeRowMap();
    }
}