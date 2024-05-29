
namespace WotBlitzStatisticsPro.Application.Mocks
{
    public class StaticDataMock : IStaticData
    {
        public Task<DictionaryVehicle[]?> GetMissedVehicles()
        {
            return Task.FromResult<DictionaryVehicle[]?>([]);
        }

        public Task<TankTreeRowMap[]?> GetTanksTreeRowMap()
        {
            return Task.FromResult<TankTreeRowMap[]?>([]);
        }
    }
}