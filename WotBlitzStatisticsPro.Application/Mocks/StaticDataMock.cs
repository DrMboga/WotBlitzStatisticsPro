namespace WotBlitzStatisticsPro.Application.Mocks
{
    public class StaticDataMock : IStaticData
    {
        public Task<TankTreeRowMap[]?> GetTanksTreeRowMap()
        {
            return Task.FromResult<TankTreeRowMap[]?>(new TankTreeRowMap[0]);
        }
    }
}