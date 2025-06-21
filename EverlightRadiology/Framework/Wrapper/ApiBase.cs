using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NUnit.Framework;
using RestSharp;
using Serilog;

namespace EverlightRadiology.Framework.Wrapper
{
    internal class ApiBase
    {
        public KeyValueFileReader ApiProperties { get; set; }
        public string Protocol { get; set; }
        public string Port { get; set; }
        public string Domain { get; set; }
        public string Endpoint { get; set; }
        public string Query { get; set; } = "";
        public RestResponse Response { get; set; } = new ();
        public RestClientOptions Options { get; set; }
        public RestClient Client { get; set; } = new();
        public RestRequest Request { get; set; } = new();
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; } = "";
        public string BodyLocation { get; set; } = "";
        public dynamic JsonFile { get; set; } = "";
        [ThreadStatic]
        public static ApiBase apiBase;

        public void AddToHeadersList(Dictionary<string, string> headers = null)
        {
            apiBase = this;
            Headers = headers ?? Headers;
            Client.AddDefaultHeaders(Headers);
        }

        public string ConstructApi(string? query = null)
        {
            Protocol = ApiProperties.GetValueOfKey("protocol");
            Port = ApiProperties.GetValueOfKey("port");
            Domain = ApiProperties.GetValueOfKey("domain");
            Endpoint = ApiProperties.GetValueOfKey("endpoint");
            Options = new RestClientOptions(new Uri(Protocol + Domain))
            {
                ThrowOnAnyError = false,
                Timeout = TimeSpan.FromSeconds(20000),
                RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true
            };
            Client = new RestClient(Options);
            //If body location is set then load payload
            AddBody();
            AddToHeadersList();
            if (Options.BaseUrl == null) return null;
            var baseUrl = Options.BaseUrl.ToString();
            return baseUrl + Endpoint + Query;
        }

        public void AddBody()
        {
            if (!BodyLocation.Equals("") && Body.Equals(""))
            {
                Body = KeyValueFileReader.ReadInWholeFile(BodyLocation);
            }
        }

        // ReSharper disable once UnusedMember.Global
        public void ExecuteGetApiCall()
        {
            Response = Client.ExecuteGetAsync(
                new RestRequest(Port + Endpoint + Query)).Result;
            Log.Debug("The response received is : {0}", Response.Content?.ToString());
        }

        // ReSharper disable once UnusedMember.Global
        public void ExecuteGetApiCallWithBody()
        {
            Response = Client.ExecuteGetAsync((
                new RestRequest(Port + Endpoint + Query))
                .AddStringBody(Body, ContentType.Json)).Result;
            Log.Debug("The response received is : {0}", Response.Content?.ToString());
        }

        public void ExecutePostApiCall()
        {
            Response = Client.ExecutePostAsync((
                new RestRequest(Port + Endpoint + Query))
                .AddStringBody(Body, ContentType.Json)).Result;
            Log.Debug("The response received is : {0}", Response.Content?.ToString());
        }

        public void ExecutePutApiCall()
        {
            Response = Client.ExecutePutAsync((
                new RestRequest(Port + Endpoint + Query))
                .AddStringBody(Body, ContentType.Json)).Result;
            Log.Debug("The response received is : {0}", Response.Content?.ToString());
        }

        public static void AssertStatusCodeReturned(int code, RestResponse response)
        {
            Assert.That(code==(int)response.StatusCode, "Status code is not: " + code);
        }

        public static void AssertMessageReturned(string message, RestResponse response)
        {
            Assert.That(message == response.StatusDescription, "Message is not: " + message);
        }

        //Turn into C# Object
        public dynamic? DeserializeJson(string payloadLocation)
        {
            var json = JsonFile.Equals("") ? KeyValueFileReader.ReadInWholeFile(payloadLocation) : (string) JsonFile;
            //string json = apiProperties.readInWholeFile(payloadLocation);
            dynamic? jsonObj = JsonConvert.DeserializeObject(json);
            return jsonObj;
        }

        //Turn into json
        public dynamic SerializeJsonAndSave(dynamic jsonObj)
        {
            jsonObj = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            JsonFile = jsonObj;
            return jsonObj;
        }

        public dynamic ModifyPayload(string nodeKeyName, string newValue, dynamic? jObj = null)
        {
            jObj ??= DeserializeJson(BodyLocation);
            var jToken = jObj?.SelectToken(nodeKeyName);
            // Update the value of the property: 
            jToken?.Replace(newValue);
            //Then make it a json object again
            return SerializeJsonAndSave(jObj);
        }

        public dynamic ModifyPayloadDeleteNode(string nodeKeyName, dynamic? jObj = null)
        {
            jObj ??= DeserializeJson(BodyLocation);
            var jToken = jObj?.SelectToken(nodeKeyName);
            // Update the value of the property: 
            jToken?.Parent?.Remove();
            //Then make it a json object again
            return SerializeJsonAndSave(jObj);
        }

        public string? ReturnValueFromPayload(string key1, string key2 = null)
        {
            var nodeString = key2 == null ? key1 : key1 + "." + key2;
            if (Response.Content == null) return null;
            var jObj = JsonConvert.DeserializeObject(Response.Content) as dynamic;
            var jToken = jObj?.SelectToken(nodeString);
            return jToken?.ToString();

        }

        public string? ReturnValueFromJsonCObj(string key, string payloadLoc)
        {
            return DeserializeJson(payloadLoc)[key];
        }

        public bool ValidateJsonSchema(string jsonSchemaFilePath)
        {
            var schema = JSchema.Parse(File.ReadAllText(jsonSchemaFilePath));
            var response = Response.Content?.ToString();
            var jsonToken = JToken.Parse(response);
            bool isValid = false;
            IList<string> errors = new List<string>();
            if (jsonToken.Type == JTokenType.Array)
            {
                var joResponse = JArray.Parse(response);
                isValid = joResponse.IsValid(schema, out errors);
            }
            else if (jsonToken.Type == JTokenType.Object)
            {
                var joResponse = JObject.Parse(response);
                isValid = joResponse.IsValid(schema, out errors);
            }
            if (!isValid)
            {
                Log.Error("Validation Errors:");
                foreach (var error in errors)
                {
                    Log.Error(error);
                }
            }
            else
            {
                Log.Information("JSON response is valid against the schema.");
            }
            return isValid;
        }

        public static void AssertNodeValue(string nodeName ,string expectedNodeValue)
        {
            var nodeActualValue = apiBase.ReturnValueFromPayload(nodeName);
            Assert.That(nodeActualValue.Equals(expectedNodeValue));
        }

        public dynamic? ConvertJsonStringToDynamic(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }
    }
}