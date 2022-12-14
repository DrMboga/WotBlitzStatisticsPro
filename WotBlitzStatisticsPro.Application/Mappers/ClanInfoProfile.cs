namespace WotBlitzStatisticsPro.Application.Mappers
{
    public static class ClanInfoProfile
    {
        public static ClanInfoDto MapToClanInfoDto(this ClanInfo clanInfo, ClanAccountInfo clanAccountInfo){
            return new ClanInfoDto {
                ClanId = clanAccountInfo?.ClanId ?? 0,
                JoinedAt = clanAccountInfo?.JoinedAt?.ToDateTime() ?? new DateTime(1970,1,1),
                Role = clanAccountInfo?.Role,
                Name = clanInfo.Name,
                CreatedAt = clanInfo.CreatedAt?.ToDateTime()?? new DateTime(1970,1,1),
                CreatorId = clanInfo.CreatorId,
                CreatorName = clanInfo.CreatorName,
                Description = clanInfo.Description,
                DescriptionHtml = clanInfo.DescriptionHtml,
                LeaderId = clanInfo.LeaderId,
                LeaderName = clanInfo.LeaderName,
                MembersCount = clanInfo.MembersCount,
                Motto = clanInfo.Motto,
                OldName = clanInfo.OldName,
                OldTag = clanInfo.OldTag,
                Tag = clanInfo.Tag,
                UpdatedAt = clanInfo.UpdatedAt?.ToDateTime()?? new DateTime(1970,1,1),
            };
        }
    }
}