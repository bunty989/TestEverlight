using EverlightRadiology.Framework.Wrapper;
using EverlightRadiology.IntegrationTests.ApiObjects.Modalities;
using EverlightRadiology.IntegrationTests.ApiObjects.Orders.PostOrders;
using System;
using TechTalk.SpecFlow;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    public class PostOrdersStepDef
    {
        protected readonly ApiContext ApiContext;
        private readonly ScenarioContext _scenarioContext;
        private readonly CommonMethods _commonMethods;
        private readonly PostOrders _postOrders;

        public PostOrdersStepDef(ScenarioContext scenarioContext, ApiContext apiContext)
        {
            ApiContext = apiContext;
            _commonMethods = new CommonMethods();
            _scenarioContext = scenarioContext;
            _postOrders = new PostOrders();
        }

        [Given(@"I hit the PostOrders endpoint with valid headers")]
        [Given(@"I hit the PostOrders endpoint with invalid body")]
        public void GivenIHitThePostOrdersEndpointWithValidHeaders()
        {
            _postOrders.PostOrderDetails();
        }

        [Given(@"I create a new order")]
        public void GivenICreateANewOrder()
        {
            _postOrders.PostOrderDetails();
            ApiContext.Response = _postOrders.Response;
            _postOrders.Body = string.Empty;
        }

        [Given(@"I modify the body for dynamic Accession Number")]
        public void GivenIModifyTheBodyForDynamicAccessionNumber()
        {
            _postOrders.ModifyPayloadBody("accessionNumber", _commonMethods.GenerateRandomString(6));
        }

        [Given(@"I modify the body for missing '([^']*)' from body")]
        public void GivenIModifyTheBodyForMissingFromBody(string nodeToRemove)
        {
            _postOrders.ModifyPayloadBodyRemoveNode(nodeToRemove);
        }

        [When(@"I get the response back from PostOrder Api")]
        public void WhenIGetTheResponseBackFromPostOrderApi()
        {
            ApiContext.Response = _postOrders.Response;
        }

        [Then(@"the response received would pass the (.*) schema check for PostOrder Api")]
        public void ThenTheResponseReceivedWouldPassTheSchemaCheckForPostOrderApi(int schemaVal)
        {
            _postOrders.PassValidSchemaCheck(schemaVal);
        }

    }
}
