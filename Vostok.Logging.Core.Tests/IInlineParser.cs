namespace Vostok.Logging.Core.Tests
{
    internal interface IInlineParser
    {
        bool TryParse(string value, out object result);
    }
}