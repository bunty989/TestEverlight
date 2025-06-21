using EverlightRadiology.Framework.Wrapper;
using EverlightRadiology.IntegrationTests.ApiObjects.Modalities;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    public class GetModalitiesStepDef
    {
        protected readonly ApiContext ApiContext;
        private readonly GetModalities _getModalities;
        private readonly ScenarioContext _scenarioContext;

        public GetModalitiesStepDef(ScenarioContext scenarioContext, ApiContext apiContext)
        {
            ApiContext = apiContext;
            _scenarioContext = scenarioContext;
            _getModalities = new GetModalities();
        }

        [Given(@"I hit the GetModalities endpoint with valid headers")]
        public void GivenIHitTheGetModalitiesEndpointWithValidHeaders()
        {
            _getModalities.GetModalityDetails();
        }

        [When(@"I get the response back from GetModalities Api")]
        public void WhenIGetTheResponseBackFromGetModalitiesApi()
        {
            ApiContext.Response = _getModalities.Response;
        }

        [Then(@"the response received would pass the schema check for GetModalities Api")]
        public void ThenTheResponseReceivedWouldPassTheSchemaCheckForGetModalitiesApi()
        {
            Assert.That(_getModalities.PassValidSchemaCheck, Is.True);
        }

    }
}
