using RestSharp;

namespace EverlightRadiology.Framework.Wrapper
{
    public class ApiContext
    {
        public RestResponse Response { get; set; }
        public string ClaimNumber { get; set; }
    }
}
