namespace WotBlitzStatisticsPro.Application.Mappers
{
    public class AccountSearchResponseProfile : Profile
    {
        public AccountSearchResponseProfile()
        {
            CreateMap<WotAccountListResponse, ShortPlayerInfoDto>()
                .ForMember(
                    d => d.AccountId, 
                    o => o.MapFrom(s => s.AccountId ?? -1));

            CreateMap<WotAccountInfo, ShortPlayerInfoDto>()
                .ForMember(dest => dest.CreatedAt,
                    o => o.MapFrom(s => s.CreatedAt.ToDateTime()))
                .ForMember(dest => dest.LastBattle,
                    o => o.MapFrom(s => s.LastBattleTime.ToDateTime()))
                .ForMember(dest => dest.Battles,
                    o => o.MapFrom(s => s.Statistics!.All!.Battles))
                .ForMember(dest => dest.WinRate,
                    o => o.MapFrom(s => s.Statistics!.All!.Battles == 0 ? 0 : 100 * s.Statistics!.All!.Wins / s.Statistics!.All!.Battles))
                ;
        }
    }
}