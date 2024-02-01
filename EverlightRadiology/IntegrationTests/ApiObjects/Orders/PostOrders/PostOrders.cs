﻿using EverlightRadiology.Framework.Wrapper;
using Serilog;

namespace EverlightRadiology.IntegrationTests.ApiObjects.Orders.PostOrders
{
    internal class PostOrders : ApiBase
    {
        public PostOrders()
        {
            var keyValFile = "IntegrationTests\\ApiObjects\\Orders\\PostOrders\\Properties\\Orders.txt";
            ApiProperties = new KeyValueFileReader(keyValFile);

            //Headers
            Headers = new Dictionary<string, string>() { };

            BodyLocation = ApiProperties.GetValueOfKey("bodyLocation");
        }

        public void ModifyPayloadBody(string key, string value)
        {
            if(!string.IsNullOrEmpty(Body)) { JsonFile = ConvertJsonStringToDynamic(Body); }
           Body = ModifyPayload(key, value, DeserializeJson(BodyLocation));
        }

        public void ModifyPayloadBodyRemoveNode(string keyToRemove)
        {
            if (!string.IsNullOrEmpty(Body)) { JsonFile = ConvertJsonStringToDynamic(Body); }
            Body = ModifyPayloadDeleteNode(keyToRemove, DeserializeJson(BodyLocation));
        }

        public void PostOrderDetails()
        {
            var endpointApi = ConstructApi();
            Log.Information("The Endpoint set for PostOrders Api is : {0}", endpointApi);
            ExecutePostApiCall();
        }

        public bool PassValidSchemaCheck(int schemaVal) 
        {
            var schemaLocation = schemaVal == 400 ? "IntegrationTests\\ApiObjects\\Orders\\PostOrders\\ResponseSchema\\Schema400.json" :
                "IntegrationTests\\ApiObjects\\Orders\\PostOrders\\ResponseSchema\\Schema409.json";
            return ValidateJsonSchema(schemaLocation); 
        }
    }
}
