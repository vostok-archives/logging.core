namespace Vostok.Logging.Core.ConversionPattern
{
    internal interface IConversionPatternParser
    {
        ConversionPattern Parse(string pattern);
    }
}