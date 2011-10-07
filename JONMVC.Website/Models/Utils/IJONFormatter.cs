namespace JONMVC.Website.Models.Utils
{
    public interface IJONFormatter
    {
        string ToCaratWeight(double weight);
        string ToCaratWeight(decimal weight);
        string ToGramWeight(double weight);
        string ToGramWeight(decimal weight);
        string ToMilimeter(double length);
        string ToMilimeter(decimal length);
        string FormatTwoDecimalPoints(double number,string ext);
        string FormatTwoDecimalPoints(decimal number, string ext);
        string FormatTwoDecimalPoints(double number);
        string FormatTwoDecimalPoints(decimal number);
    }
}