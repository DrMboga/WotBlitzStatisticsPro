using AutoMapper;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.WargamingApi.Model;

namespace WotBlitzStatisticsPro.Application.Mappers
{
    public class AccountSearchResponseProfile : Profile
    {
        public AccountSearchResponseProfile()
        {
            CreateMap<WotAccountListResponse, PlayerInfoDto>()
                .ForMember(
                    d => d.AccountId, 
                    o => o.MapFrom(s => s.AccountId ?? -1));

            // CreateMap<WotAccountInfo, PlayerInfoDto>()
            //     .ForMember(dest => dest.LastBattle,
            //         o => o.MapFrom(s => s.LastBattleTime.ToDateTime()))
            //     .ForMember(dest => dest.BattlesCount,
            //         o => o.MapFrom(s => s.Statistics!.All!.Battles))
            //     .ForMember(dest => dest.WinRate,
            //         o => o.MapFrom(s => s.Statistics!.All!.Battles == 0 ? 0 : 100 * s.Statistics!.All!.Wins / s.Statistics!.All!.Battles))
            //     ;
        }
    }
}