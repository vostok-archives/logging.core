namespace Vostok.Logging.Core
{
    public interface ILogSettingsValidator<in TSettings>
    {
        SettingsValidationResult TryValidate(TSettings settings);
    }
}