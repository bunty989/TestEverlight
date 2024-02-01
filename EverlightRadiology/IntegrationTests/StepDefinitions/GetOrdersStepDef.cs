using EverlightRadiology.Framework.Wrapper;
using EverlightRadiology.IntegrationTests.ApiObjects.Orders.GetOrders;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    internal class GetOrdersStepDef
    {
        GetOrders _getOrders = new();
        protected readonly ApiContext ApiContext;
        private readonly ScenarioContext _scenarioContext;

        public GetOrdersStepDef(ScenarioContext scenarioContext, ApiContext apiContext)
        {
            ApiContext = apiContext;
            _scenarioContext = scenarioContext;
        }

        [Given(@"I hit the GetOrders endpoint with valid headers")]
        public void GivenIHitTheEndpointWithValidHeaders()
        {
            _getOrders.GetOrderDetails();
        }

        [When(@"I get the response back from GetOrder Api")]
        public void WhenIGetTheResponseBackFromGetOrderApi()
        {
            ApiContext.Response = _getOrders.Response;
        }

        [Then(@"I will recieve an '([^']*)' response")]
        public void ThenIWillRecieveAnResponse(string message)
        {
            ApiBase.AssertMessageReturned(message, ApiContext.Response);
        }

        [Then(@"the response received would pass the schema check for GetOrder Api")]
        public void ThenTheResponseReceivedWouldPassTheSchemaCheck()
        {
            Assert.That(_getOrders.PassValidSchemaCheck,Is.True);
        }
    }
}
