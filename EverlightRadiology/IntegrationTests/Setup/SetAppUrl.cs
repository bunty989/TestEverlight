using Serilog;
using EverlightRadiology.Framework.Wrapper;
using ConfigType = EverlightRadiology.Framework.Wrapper.TestConstant.ConfigTypes;
using ConfigKey = EverlightRadiology.Framework.Wrapper.TestConstant.ConfigTypesKey;

namespace EverlightRadiology.IntegrationTests.Setup
{
    internal class SetAppUrl
    {
        public static string SetUrl(string protocol)
        {
            var url = ConfigHelper.ReadConfigValue(ConfigType.AppConfig, ConfigKey.AppUrl);
            protocol = protocol.ToLower() switch
            {
                "http" => "http://",
                "https" => "https://",
                _ => protocol
            };
            Log.Information("The Application URl for the application is set as {0}", protocol + url);
            return protocol + url;
        }
    }
}
