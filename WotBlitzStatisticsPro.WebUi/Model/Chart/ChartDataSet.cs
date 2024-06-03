namespace WotBlitzStatisticsPro.WebUi;

public class ChartDataSet
{
    public string Label { get; set; } = string.Empty;
    public decimal[]? Data { get; set; }
    public string BorderColor { get; set; } = string.Empty;
    public string BackgroundColor { get; set; } = string.Empty;
}
