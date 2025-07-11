using EverlightRadiology.Framework.Wrapper;
using EverlightRadiology.IntegrationTests.ApiObjects.Clients;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    public class GetClientsStepDef
    {
        protected readonly ApiContext ApiContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly GetClients _getClients;

        public GetClientsStepDef(ScenarioContext scenarioContext, ApiContext apiContext)
        {
            ApiContext = apiContext;
            _scenarioContext = scenarioContext;
            _getClients = new GetClients();
        }

        [Given(@"I hit the GetClient endpoint with valid headers")]
        public void GivenIHitTheGetClientEndpointWithValidHeaders()
        {
            _getClients.GetClientDetails();
        }

        [When(@"I get the response back from GetClient Api")]
        public void WhenIGetTheResponseBackFromGetClientApi()
        {
            ApiContext.Response = _getClients.Response;
        }

        [Then(@"the response received would pass the schema check for GetClient Api")]
        public void ThenTheResponseReceivedWouldPassTheSchemaCheckForGetClientApi()
        {
            Assert.That(_getClients.PassValidSchemaCheck, Is.True);
        }

    }
}
