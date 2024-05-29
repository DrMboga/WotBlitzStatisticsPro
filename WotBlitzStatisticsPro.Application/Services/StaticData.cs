using System.Net.Http.Json;

namespace WotBlitzStatisticsPro.Application.Services
{
    public class StaticData: IStaticData
    {
        private readonly HttpClient _httpClient;

        public StaticData(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<DictionaryVehicle[]?> GetMissedVehicles()
        {
            return _httpClient.GetFromJsonAsync<DictionaryVehicle[]>("missed-tanks.json");
        }

        public Task<TankTreeRowMap[]?> GetTanksTreeRowMap()
        {
            return _httpClient.GetFromJsonAsync<TankTreeRowMap[]>("tank-tree-row-map.json");
        }

    }
}