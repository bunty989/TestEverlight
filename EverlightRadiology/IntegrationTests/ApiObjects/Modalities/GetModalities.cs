using EverlightRadiology.Framework.Wrapper;
using Serilog;

namespace EverlightRadiology.IntegrationTests.ApiObjects.Modalities
{
    internal class GetModalities : ApiBase
    {
        public GetModalities()
        {
            var keyValFile = Path.Combine("IntegrationTests", "ApiObjects", "Modalities", "Properties", "Modalities.txt");
            ApiProperties = new KeyValueFileReader(keyValFile);

            //Headers
            Headers = new Dictionary<string, string>() { };
        }
        
        public void GetModalityDetails()
        {
            var endpointApi = ConstructApi();
            Log.Information("The Endpoint set for GetModalities Api is : {0}", endpointApi);
            ExecuteGetApiCall();
        }

        public bool PassValidSchemaCheck() { return ValidateJsonSchema(Path.Combine("IntegrationTests", "ApiObjects", "Modalities", "ResponseSchema", "Schema200.json")); }
    }
}
