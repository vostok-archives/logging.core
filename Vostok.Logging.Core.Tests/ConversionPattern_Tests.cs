using NUnit.Framework;
// ReSharper disable UseStringInterpolation

namespace Vostok.Logging.Core.Tests
{
    [TestFixture]
    internal class ConversionPattern_Tests
    {
        /*[Test]
        public void Format_should_render_substrings_after_keys_if_event_has_such_properties()
        {
            var dateTime = DateTimeOffset.UtcNow;
            const LogLevel level = LogLevel.Info;
            const string prefix = "p";
            const string message = "Hello, World";
            var exception = new Exception("AnyException");
            const string property = "value";
            var properties = new Dictionary<string, object> { { "prefix", new []{ prefix } } , { "prop", property }};

            var pattern = ConversionPattern.FromString("a%da%la%xa%ma%ea%pa%p(prop)a%n");

            var @event = GenerateEvent(dateTime, message, exception, properties);
            var template = string.Format("a{0:HH:mm:ss zzz}a{1}a[{2}]a{3}a{4}a[properties: {5}]a{6}a\r\n",
                dateTime, 
                level,
                prefix,
                message,
                exception, 
                string.Join(", ", properties.Select(p => $"{ p.Key} = { p.Value}")),
                property);

            pattern.Format(@event).Should().Be(template);
        }

        [Test]
        public void Format_should_render_only_template_string_if_it_not_contains_any_keys()
        {
            var dateTime = DateTimeOffset.UtcNow;
            const string prefix = "p";
            const string message = "Hello, World";
            var exception = new Exception("AnyException");
            const string property = "value";
            var properties = new Dictionary<string, object> { { "prefix", new[] { prefix } }, { "prop", property } };

            var pattern = ConversionPattern.FromString("aaa");

            var @event = GenerateEvent(dateTime, message, exception, properties);

            pattern.Format(@event).Should().Be("aaa");
        }

        [Test]
        public void Format_should_not_render_substrings_after_keys_if_event_has_not_such_properties()
        {
            var dateTime = DateTimeOffset.UtcNow;
            const LogLevel level = LogLevel.Info;

            var pattern = ConversionPattern.FromString("a%da%la%xa%ma%ea%pa%p(prop)a%n");

            var @event = GenerateEvent(dateTime, null, null, null);

            var template = string.Format("a{0:HH:mm:ss zzz}a{1}a\r\n", dateTime, level);
            pattern.Format(@event).Should().Be(template);
        }

        private static LogEvent GenerateEvent(LogLevel level = LogLevel.Info)
        {
            return new LogEvent(level, DateTimeOffset.UtcNow, null);
        }

        private static LogEvent GenerateEvent(Exception exception)
        {
            return new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, null, exception);
        }

        private static LogEvent GenerateEvent(IReadOnlyDictionary<string, object> properties)
        {
            return properties.Aggregate(new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, null), (e, prop) => e.WithProperty(prop.Key, prop.Value));
        }

        private static LogEvent GenerateEvent(string messageTemplate, IReadOnlyDictionary<string, object> properties = null)
        {
            var @event = new LogEvent(LogLevel.Info, DateTimeOffset.UtcNow, messageTemplate);
            return properties?.Aggregate(@event, (e, prop) => e.WithProperty(prop.Key, prop.Value)) ?? @event;
        }

        private static LogEvent GenerateEvent(DateTimeOffset timestamp, string messageTemplate, Exception exception, IReadOnlyDictionary<string, object> properties)
        {
            var @event = new LogEvent(LogLevel.Info, timestamp, messageTemplate, exception);
            return properties?.Aggregate(@event, (e, prop) => e.WithProperty(prop.Key, prop.Value)) ?? @event;
        }*/
    }
}