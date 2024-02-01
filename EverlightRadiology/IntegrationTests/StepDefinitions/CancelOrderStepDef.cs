using EverlightRadiology.Framework.Drivers;
using EverlightRadiology.IntegrationTests.PageObjects;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    public class CancelOrderStepDef
    {
        private readonly OrdersPage? _ordersPage;
        private readonly ScenarioContext? _scenarioContext;

        public CancelOrderStepDef(ScenarioContext scenarioContext,
            DriverHelper driverHelper)
        {
            var driver = driverHelper.Driver;
            _scenarioContext = scenarioContext;
            _ordersPage = new OrdersPage(driver);
        }

        [When(@"the order is successfully created")]
        public void WhenTheOrderIsSuccessfullyCreated() =>
            _scenarioContext?
            .Set<string>(_ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"),
                "Org Code"), "OrgCode");

        [When(@"I click on the X button and approve the confirmation popup")]
        public void WhenIClickOnTheXButtonAndApproveTheConfirmationPopup()
        {
            _ordersPage?.ClickXButton(_scenarioContext?.Get<string>("AccessionNum"));
            _ordersPage?.AcceptPopup();
        }

        [When(@"I select an order to cancel")]
        public void WhenISelectAnOrderToCancel()
        {
            _scenarioContext?.Set<string>(_ordersPage?.GetOrderDetails("00494", "Org Code"), "OrgCode");
        }

        [When(@"I click on the X button and decline the confirmation popup")]
        public void WhenIClickOnTheXButtonAndDeclineTheConfirmationPopup()
        {
            _ordersPage?.ClickXButton("00494");
            _ordersPage?.CancelPopup();
        }

        [Then(@"the order is not cancelled")]
        public void ThenTheOrderIsNotCancelled()
        {
            _ordersPage?.RefreshPage();
            var orgCode = _ordersPage?.GetOrderDetails("00494", "Org Code");
            Assert.That(orgCode == _scenarioContext.Get<string>("OrgCode"));
        }


        [Then(@"the order is successfully cancelled and deleted")]
        public void ThenTheOrderIsSuccessfullyCancelledAndDeleted()
        {
            _ordersPage?.RefreshPage();
            var orgCode = _ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"), "Org Code");
            Assert.That(string.IsNullOrEmpty(orgCode) && _scenarioContext.Get<string>("OrgCode").Equals("CUK"));
        }
    }
}
