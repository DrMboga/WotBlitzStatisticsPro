using AutoMapper;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Helpers;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.Application.Mappers
{
    public class AccountInfoProfile: Profile
    {
        public AccountInfoProfile()
        {
            CreateMap<WotAccountInfo, PlayerInfoDto>()
                .ForMember(dest => dest.CreatedAt,
                    o => o.MapFrom(s => s.CreatedAt.ToDateTime()))
                .ForMember(dest => dest.LastBattleTime,
                    o => o.MapFrom(s => s.LastBattleTime.ToDateTime()));

            CreateMap<WotAccountFullStatistics, PlayerInfoDto>();
        }
    }
}