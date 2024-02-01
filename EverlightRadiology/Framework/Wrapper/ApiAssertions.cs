//using Newtonsoft.Json.Linq;
//using Newtonsoft.Json.Schema;
//using NUnit.Framework;
//using RestSharp;
//using Serilog;
//using ContentType = EverlightRadiology.Framework.Wrapper.TestConstant.ContentType;

//namespace EverlightRadiology.Framework.Wrapper
//{
//    internal class ApiAssertions
//    {
//        public static void CheckStatus(int actualStatusCode, int expectedStatusCode)
//        {
//            Assert.That(actualStatusCode.Equals(expectedStatusCode));
//        }

//        public static void CheckContentType(RestResponse response)
//        {
//            switch (response.ContentType)
//            {
//                case ContentType.JSON:
//                    {
//                        Assert.That(response.ContentType.Equals(ContentType.JSON));
//                        break;
//                    }
//                case ContentType.XML:
//                    {
//                        Assert.That(response.ContentType.Equals(ContentType.XML));
//                        break;
//                    }
//                case ContentType.URL_ENCODED:
//                    {
//                        Assert.That(response.ContentType.Equals(ContentType.URL_ENCODED));
//                        break;
//                    }
//                default:
//                    {
//                        Assert.That(response.ContentType.Equals("application/json"));
//                        break;
//                    }
//            };
//        }

//        public static void CheckResponseSchema(RestResponse response, String filePath)
//        {
//            var schema = JSchema.Parse(File.ReadAllText(filePath));
//            var joResponse = JObject.Parse(response.Content.ToString());
//            IList<string> errors;
//            bool isValid = joResponse.IsValid(schema, out errors);
//            if (!isValid)
//            {
//                Log.Error("Validation Errors:");
//                foreach (var error in errors)
//                {
//                    Log.Error(error);
//                }
//            }
//            else
//            {
//                Log.Debug("JSON response is valid against the schema.");
//            }
//        }
//    }
//}
