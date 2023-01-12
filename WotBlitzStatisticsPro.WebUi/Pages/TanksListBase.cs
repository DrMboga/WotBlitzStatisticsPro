using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.WebUi.Model;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WebUi.Helpers;
using Microsoft.AspNetCore.Components.Web;
using MediatR;

namespace WotBlitzStatisticsPro.WebUi.Pages
{
    public class TanksListBase: ComponentBase
    {
        public enum TanksSorting
        {
            LastBattle,
            WinRate,
            Battles,
            Damage,
            Wn7,
            Tier
        }

        [Parameter]
        public TankInfoDto[]? TanksList { get; set; }

        [Inject]
        public IStringLocalizer<App>? Localize { get; set; }

        [Inject]
        public IMediator? Mediator { get; set; }

        public IEnumerable<TankInfoDto>? FilteredTankList { get; set; }

        public List<FilterItem<int>> TierFilter { get; set; } = new();
        public IEnumerable<int> FilteredTiers { get; set; } = new int[] { };

        public List<FilterItem<string>> TankTypesFilter { get; set; } = new();
        public IEnumerable<string> FilteredTankTypes { get; set; } = new string[] { };

        public bool? FilterIsPremium { get; set; } = null;

        public List<FilterItem<string>> NationsFilter { get; set; } = new();
        public IEnumerable<string> FilteredNations { get; set; } = new string[] { };

        public List<FilterItem<MarkOfMastery>> MasteryFilter { get; set; } = new();
        public IEnumerable<MarkOfMastery> FilteredMastery { get; set; } = new MarkOfMastery[] { };

        public int TotalBattles { get; set; }
        public int AvgWinRate { get; set; }
        public double AvgWn7 { get; set; }
        public int AvgTotalDamage { get; set; }
        public int AvgTotalXp { get; set; }
        public int AvgSurvivalRate { get; set; }
        public int? MinBattles { get; set; } = null;

        public TanksSorting sorting { get; set; } = TanksSorting.LastBattle;

        public bool isSortAscending { get; set; } = false;


        protected override void OnInitialized()
        {
            FilteredTankList = TanksList?.OrderByDescending(t => t.LastBattleTime);
            CountTotalParams();
            for (int i = 1; i < 11; i++)
            {
                TierFilter.Add(new FilterItem<int>(i, i.RomanNumber()));
            }
            FillTankTypesFilter();
            FillNationsFilter();
            FillMasteryFilter();
        }

        public void OnFilterChange(object? value)
        {
            FilteredTankList = TanksList;
            if (FilteredTiers.Any())
            {
                FilteredTankList = FilteredTankList?.Where(t => FilteredTiers.Contains(t.Tier));
            }
            if (FilteredTankTypes.Any())
            {
                FilteredTankList = FilteredTankList?.Where(t => FilteredTankTypes.Contains(t.TankType));
            }
            if (FilterIsPremium.HasValue)
            {
                FilteredTankList = FilteredTankList?.Where(t => t.IsPremium == FilterIsPremium.Value);
            }
            if (FilteredNations.Any())
            {
                FilteredTankList = FilteredTankList?.Where(t => FilteredNations.Contains(t.TankNation));
            }
            if (FilteredMastery.Any())
            {
                FilteredTankList = FilteredTankList?.Where(t => FilteredMastery.Contains(t.MarkOfMastery));
            }
            if (MinBattles.HasValue)
            {
                FilteredTankList = FilteredTankList?.Where(t => t.Battles > MinBattles.Value);
            }

            CountTotalParams();
        }

        private void FillTankTypesFilter()
        {
            // ToDo: Should get values from Dictionary here instead of hard code
            TankTypesFilter.Add(new FilterItem<string>(Constants.HeavyTank, Constants.HeavyTank.TankTypeAsset(false)));
            TankTypesFilter.Add(new FilterItem<string>(Constants.AtSpg, Constants.AtSpg.TankTypeAsset(false)));
            TankTypesFilter.Add(new FilterItem<string>(Constants.MediumTank, Constants.MediumTank.TankTypeAsset(false)));
            TankTypesFilter.Add(new FilterItem<string>(Constants.LightTank, Constants.LightTank.TankTypeAsset(false)));
        }

        private void FillNationsFilter()
        {
            // ToDo: Should get values from Dictionary here instead of hard code
            NationsFilter.Add(new FilterItem<string>(Constants.CountryUsa, Constants.CountryUsa.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryFrance, Constants.CountryFrance.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryUssr, Constants.CountryUssr.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryChina, Constants.CountryChina.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryUk, Constants.CountryUk.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryJapan, Constants.CountryJapan.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryGermany, Constants.CountryGermany.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryOther, Constants.CountryOther.NationAsset()));
            NationsFilter.Add(new FilterItem<string>(Constants.CountryEuropean, Constants.CountryEuropean.NationAsset()));
        }

        private void FillMasteryFilter()
        {
            foreach (MarkOfMastery mastery in Enum.GetValues(typeof(MarkOfMastery)))
            {
                MasteryFilter.Add(new FilterItem<MarkOfMastery>(mastery, mastery.IconAsset()));
            }
        }

        private void CountTotalParams()
        {
            if(FilteredTankList == null)
            {
                return;
            }
            TotalBattles = Convert.ToInt32(FilteredTankList?.Sum(t => t.Battles));
            if (TotalBattles > 0)
            {
                AvgWinRate = Convert.ToInt32(100 * FilteredTankList!.Sum(t => t.Wins) / TotalBattles);
                AvgWn7 = Convert.ToDouble(FilteredTankList!.Sum(t => t.Wn7)) / FilteredTankList!.Count();
                AvgTotalDamage = Convert.ToInt32(FilteredTankList!.Sum(t => t.DamageDealt) / TotalBattles);
                AvgTotalXp = Convert.ToInt32(FilteredTankList!.Sum(t => t.Xp) / TotalBattles);
                AvgSurvivalRate = Convert.ToInt32(100 * FilteredTankList!.Sum(t => t.WinAndSurvived) / TotalBattles);
            }
            else
            {
                AvgWinRate = 0;
                AvgWn7 = 0;
                AvgTotalDamage = 0;
                AvgTotalXp = 0;
                AvgSurvivalRate = 0;
            }
        }

        public void OnFilterClick(MouseEventArgs e, TanksSorting sortingToApply)
        {
            if (sortingToApply != sorting)
            {
                sorting = sortingToApply;
                isSortAscending = false;
            }
            else
            {
                isSortAscending = !isSortAscending;
            }
            FilterTanksList();
        }

        public void FilterTanksList()
        {
            if(FilteredTankList == null)
            {
                return;
            }
            switch (sorting) 
            {
                case TanksSorting.LastBattle:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.LastBattleTime) : FilteredTankList.OrderByDescending(t => t.LastBattleTime);
                    break;
                case TanksSorting.WinRate:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.WinRate) : FilteredTankList.OrderByDescending(t => t.WinRate);
                    break;
                case TanksSorting.Battles:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.Battles) : FilteredTankList.OrderByDescending(t => t.Battles);
                    break;
                case TanksSorting.Damage:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.AvgDamage) : FilteredTankList.OrderByDescending(t => t.AvgDamage);
                    break;
                case TanksSorting.Wn7:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.Wn7) : FilteredTankList.OrderByDescending(t => t.Wn7);
                    break;
                case TanksSorting.Tier:
                    FilteredTankList = isSortAscending ? FilteredTankList.OrderBy(t => t.Tier) : FilteredTankList.OrderByDescending(t => t.Tier);
                    break;
            }
        }

        public void ApplyNationFilter(MouseEventArgs e, string nationToApply)
        {
            if(FilteredNations.Contains(nationToApply))
            {
                FilteredNations = FilteredNations.Where(n => n != nationToApply).ToArray();
            }
            else
            {
                var nations = FilteredNations.ToList();
                nations.Add(nationToApply);
                FilteredNations = nations.ToArray();
            }

            OnFilterChange(null);
        }

        public string NationFilterStyle(string nation)
        {
            string basic = "nation-filter-flag";
            if(FilteredNations.Contains(nation))
            {
                return $"{basic} filter-selected";
            }
            return basic;
        }

        public void ApplyTierFilter(MouseEventArgs e, int tierToApply)
        {
            if(FilteredTiers.Contains(tierToApply))
            {
                FilteredTiers = FilteredTiers.Where(n => n != tierToApply).ToArray();
            }
            else
            {
                var tiers = FilteredTiers.ToList();
                tiers.Add(tierToApply);
                FilteredTiers = tiers.ToArray();
            }

            OnFilterChange(null);
        }

        public string TierFilterStyle(int tier)
        {
            string basic = "tier-filter";
            if(FilteredTiers.Contains(tier))
            {
                return $"{basic} filter-selected";
            }
            return basic;
        }

        public void ApplyTankTypeFilter(MouseEventArgs e, string typeToApply)
        {
            if(FilteredTankTypes.Contains(typeToApply))
            {
                FilteredTankTypes = FilteredTankTypes.Where(n => n != typeToApply).ToArray();
            }
            else
            {
                var types = FilteredTankTypes.ToList();
                types.Add(typeToApply);
                FilteredTankTypes = types.ToArray();
            }

            OnFilterChange(null);
        }

        public string TankTypeFilterStyle(string typeToApply)
        {
            string basic = "tier-filter";
            if(FilteredTankTypes.Contains(typeToApply))
            {
                return $"{basic} filter-selected";
            }
            return basic;
        }

        public void ApplyPremiumFilter(MouseEventArgs e)
        {
            if(FilterIsPremium.HasValue && FilterIsPremium.Value == true)
            {
                FilterIsPremium = false;
            }
            else if(FilterIsPremium.HasValue && FilterIsPremium.Value == false)
            {
                FilterIsPremium = null;
            }
            else if(!FilterIsPremium.HasValue)
            {
                FilterIsPremium = true;
            }
            OnFilterChange(null);
        }

        public string PremiumStyle()
        {
            string basic = "tier-filter";
            if(FilterIsPremium.HasValue && FilterIsPremium.Value == true)
            {
                return $"{basic} filter-selected";
            }
            if(FilterIsPremium.HasValue && FilterIsPremium.Value == false)
            {
                return $"{basic} filter-non-premium-selected";
            }
            return basic;
        }

        public void ApplyMasteryFilter(MouseEventArgs e, MarkOfMastery mastery)
        {
            if(FilteredMastery.Contains(mastery))
            {
                FilteredMastery = FilteredMastery.Where(n => n != mastery).ToArray();
            }
            else
            {
                var masteries = FilteredMastery.ToList();
                masteries.Add(mastery);
                FilteredMastery = masteries.ToArray();
            }

            OnFilterChange(null);
        }

        public string MasteryFilterStyle(MarkOfMastery mastery)
        {
            string basic = "tier-filter";
            if(FilteredMastery.Contains(mastery))
            {
                return $"{basic} filter-selected";
            }
            return basic;
        }
    }
}