namespace Vostok.Logging.Core
{
    internal interface IConversionPatternParser
    {
        ConversionPattern Parse(string pattern);
    }
}