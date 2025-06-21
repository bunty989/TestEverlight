using EverlightRadiology.Framework.Wrapper;
using Serilog;

namespace EverlightRadiology.IntegrationTests.ApiObjects.Clients
{
    internal class GetClients : ApiBase
    {
        public GetClients()
        {
            var keyValFile = Path.Combine("IntegrationTests", "ApiObjects", "Clients", "Properties", "Clients.txt");
            ApiProperties = new KeyValueFileReader(keyValFile);

            //Headers
            Headers = new Dictionary<string, string>() { };
        }
        
        public void GetClientDetails()
        {
            var endpointApi = ConstructApi();
            Log.Information("The Endpoint set for GetClient Api is : {0}", endpointApi);
            ExecuteGetApiCall();
        }

        public bool PassValidSchemaCheck()
        {
            var schemaPath = Path.Combine("IntegrationTests", "ApiObjects", "Clients", "ResponseSchema", "Schema200.json");
            return ValidateJsonSchema(schemaPath);
        }
    }
}
