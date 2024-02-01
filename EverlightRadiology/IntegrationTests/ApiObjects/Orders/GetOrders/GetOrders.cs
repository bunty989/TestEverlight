using EverlightRadiology.Framework.Wrapper;
using Serilog;

namespace EverlightRadiology.IntegrationTests.ApiObjects.Orders.GetOrders
{
    internal class GetOrders : ApiBase
    {
        public GetOrders()
        {
            var keyValFile = "IntegrationTests\\ApiObjects\\Orders\\GetOrders\\Properties\\Orders.txt";
            ApiProperties = new KeyValueFileReader(keyValFile);

            //Headers
            Headers = new Dictionary<string, string>() { };
        }

        public void GetOrderDetails()
        {
            var endpointApi = ConstructApi();
            Log.Information("The Endpoint set for GetOrders Api is : {0}", endpointApi);
            ExecuteGetApiCall();
        }

        public bool PassValidSchemaCheck() { return ValidateJsonSchema("IntegrationTests\\ApiObjects\\Orders\\GetOrders\\ResponseSchema\\Schema200.json"); }
    }
}
