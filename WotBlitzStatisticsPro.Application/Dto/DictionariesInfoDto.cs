namespace WotBlitzStatisticsPro.Application.Dto
{
    public record DictionariesInfoDto(
        DateTime LastUpdateDate, 
        string GameVersion, 
        string DictionariesLanguage, 
        long? LoggedInAccountId,
        string? WgToken,
        DateTime? WgTokenExpiration);
}