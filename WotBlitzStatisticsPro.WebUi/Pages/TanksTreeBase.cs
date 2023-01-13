using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.Localization;
using WotBlitzStatisticsPro.Application.Dto;
using WotBlitzStatisticsPro.Application.Messages;
using WotBlitzStatisticsPro.Model;
using WotBlitzStatisticsPro.WebUi.Helpers;
using WotBlitzStatisticsPro.WebUi.Model;
using WotBlitzStatisticsPro.WebUi.Services;

namespace WotBlitzStatisticsPro.WebUi.Pages
{
    public class TanksTreeBase: ComponentBase
    {
        [Parameter]
        public TankInfoDto[]? TanksList { get; set; }

        [Inject]
        public IStringLocalizer<App>? Localize { get; set; }

        [Inject]
        public IMediator? Mediator { get; set; }

        [Inject]
        public ISvgHelper? SvgHelper { get; set; }

        public DictionaryVehicleDto[]? VehiclesLibrary { get; set; }

        public int FrameHeight { get; set; }

        public int CardWidth { get; } = 200;
        public int CardHeight { get; } = 120;

        public int LeftMargin { get; } = 30;

        public List<TankTreeItem> TreeItems { get; set; } = new();

        public string SelectedNation { get; set; } = string.Empty;

        public List<string> Connections { get; set; } = new List<string>();

        public List<string> ConnectionsInVerticalView { get; set; } = new List<string>();

        public List<FilterItem<string>> NationsFilter { get; set; } = new();

        public string FrameStyle
        {
            get
            {
                return $"min-height: {FrameHeight}px; overflow-y: auto; overflow-x: auto;";
            }
        }

        protected async override Task OnInitializedAsync()
        {
            FillNationsFilter();

            await LoadTree();
        }

        public int GetTextWidth(string text, string fontFace, int fontSize)
        {
            return SvgHelper?.CalculateTextBlockSize(text, fontFace, fontSize).Width ?? 0;
        }

        private void BuildTree()
        {
            if(VehiclesLibrary == null)
            {
                return;
            }

            TreeItems.Clear();
            Connections.Clear();
            ConnectionsInVerticalView.Clear();
            var vehiclesTree = VehiclesLibrary.Where(v => v != null && v.IsPremium == false).ToList();
            for (int tier = 1; tier < 11; tier++)
            {
                var vehiclesByTier = vehiclesTree.Where(v => v.Tier == tier).ToList();
                foreach (var vehicleFromDictionary in vehiclesByTier)
                {
                    var treeItem = BuildNonPremTreeItem(vehicleFromDictionary);
                    if(treeItem.NextRows != null)
                    {
                        foreach (var nextRow in treeItem.NextRows)
                        {
                            Connections.Add(SvgHelper?.TankTreeConnectionPath(treeItem.Row, nextRow.Row, tier, CardWidth, CardHeight) ?? string.Empty);
                            ConnectionsInVerticalView.Add(SvgHelper?.TankTreeConnectionVerticalPath(treeItem.Row, nextRow.Row, tier, CardWidth, CardHeight, LeftMargin) ?? string.Empty);
                        }
                    }
                    
                    TreeItems.Add(treeItem);
                }
            }

            int maxNonPremTreeRow = GetMaxRowNumber(TreeItems);
            
            if(TanksList == null)
            {
                return;
            }
            
            // Adding prem tanks
            var premTanks = TanksList.Where(t => t.IsPremium && t.TankNation == SelectedNation).ToList();
            int maxPremTanksCount = maxNonPremTreeRow;
            foreach (var premTier in premTanks.Select(b => b.Tier).Distinct())
            {
                int row = maxNonPremTreeRow + 1;
                foreach (var prem in premTanks.Where(t => t.Tier == premTier))
                {
                    TreeItems.Add(new TankTreeItem
                    {
                        Tier = prem.Tier,
                        TankId = prem.TankId,
                        Name = prem.Name ?? string.Empty,
                        PreviewImage = prem.PreviewImage?.ToLocalVehiclesUrl() ?? string.Empty,
                        TankTypeId = prem.TankType ?? string.Empty,
                        IsPremium = true,
                        MarkOfMastery = prem.MarkOfMastery,
                        WinRate = prem.WinRate,
                        AvgDamage = prem.AvgDamage,
                        Battles = prem.Battles,
                        LastBattleTime = prem.LastBattleTime,
                        IsResearched = true,
                        Row = row
                    });
                    row++;
                    if (row > maxPremTanksCount)
                    {
                        maxPremTanksCount = row;
                    }
                }
            }

            FrameHeight = maxPremTanksCount * (CardHeight + 20) + 20;
        }

        private TankTreeItem BuildNonPremTreeItem(DictionaryVehicleDto vehicleFromDictionary)
        {
            var treeItem = new TankTreeItem
            {
                Tier = vehicleFromDictionary.Tier,
                TankId = vehicleFromDictionary.TankId,
                Name = vehicleFromDictionary.Name,
                PriceCredit = vehicleFromDictionary.PriceCredits,
                PreviewImage = vehicleFromDictionary.PreviewImage?.ToLocalVehiclesUrl() ?? string.Empty,
                TankTypeId = vehicleFromDictionary.Type,
                IsPremium = false,
                Row = vehicleFromDictionary.CurrentTankTreeRow
            };
            var researched = TanksList?.FirstOrDefault(t => t.TankId == treeItem.TankId);
            treeItem.IsResearched = researched != null;
            if (researched != null)
            {
                treeItem.MarkOfMastery = researched.MarkOfMastery;
                treeItem.WinRate = researched.WinRate;
                treeItem.AvgDamage = researched.AvgDamage;
                treeItem.Battles = researched.Battles;
                treeItem.LastBattleTime = researched.LastBattleTime;
            }

            treeItem.NextRows = vehicleFromDictionary?
                .nextTanks?
                .Select(t => new TankTreeRowMap(t.NextTankId, t.TreeRowIndex))
                .ToList();

            return treeItem;
        }

        private int GetMaxRowNumber(List<TankTreeItem> branch)
        {
            int maxRowsCount = 0;
            foreach (var tier in branch.Select(b => b.Tier).Distinct())
            {
                int tierTanksCount = branch.Count(t => t.Tier == tier);
                if (tierTanksCount > maxRowsCount)
                {
                    maxRowsCount = tierTanksCount;
                }
            }

            return maxRowsCount;
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

            SelectedNation = NationsFilter[0].ItemId;
        }

        public string NationFilterStyle(string nation)
        {
            string basic = "nation-filter-flag";
            if (SelectedNation == nation)
            {
                return $"{basic} filter-selected";
            }
            return basic;
        }

        public async Task ApplyNationFilter(MouseEventArgs e, string nationToApply)
        {
            SelectedNation = nationToApply;
            await LoadTree();
        }

        private async Task LoadTree()
        {
            if(Mediator == null)
            {
                return;
            }
            VehiclesLibrary = await Mediator.Send(new GetDictionaryVehiclesByNationRequest(SelectedNation));
            BuildTree();
        }
    }
}