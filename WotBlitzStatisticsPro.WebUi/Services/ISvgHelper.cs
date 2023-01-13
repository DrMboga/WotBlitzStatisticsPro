using WotBlitzStatisticsPro.WebUi.Model;

namespace WotBlitzStatisticsPro.WebUi.Services
{
    public interface ISvgHelper
    {
        ElementDimensions CalculateTextBlockSize(string text, string fontFace, int fontSize);

        string TankTreeConnectionPath(int rowStart, int rowEnd, int tierStart, int cardWidth, int cardHeight);

        string TankTreeConnectionVerticalPath(int rowStart, int rowEnd, int tierStart, int cardWidth, int cardHeight, int leftMargin);

    }
}