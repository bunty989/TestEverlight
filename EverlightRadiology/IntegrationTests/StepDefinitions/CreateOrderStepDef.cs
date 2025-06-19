using EverlightRadiology.Framework.Drivers;
using EverlightRadiology.Framework.Wrapper;
using EverlightRadiology.IntegrationTests.PageObjects;
using EverlightRadiology.IntegrationTests.Setup;
using TechTalk.SpecFlow;
using ConfigType = EverlightRadiology.Framework.Wrapper.TestConstant.ConfigTypes;
using ConfigKey = EverlightRadiology.Framework.Wrapper.TestConstant.ConfigTypesKey;
using NUnit.Framework;

namespace EverlightRadiology.IntegrationTests.StepDefinitions
{
    [Binding]
    public class CreateOrderStepDef
    {
        private readonly HomePage? _homePage;
        private readonly OrdersPage? _ordersPage;
        private readonly NewOrderPage? _newOrderPage;
        private readonly DriverHelper? _driverHelper;
        private readonly CommonMethods? _commonMethods;
        private readonly ScenarioContext? _scenarioContext;
        private static string? Protocol => ConfigHelper.ReadConfigValue(ConfigType.WebDriverConfig,
            ConfigKey.Protocol);
        private static string Url => SetAppUrl.SetUrl(Protocol);

        public CreateOrderStepDef(ScenarioContext scenarioContext,
            DriverHelper driverHelper)
        {
            _driverHelper = driverHelper;
            var driver = driverHelper.Driver;
            _commonMethods = new CommonMethods();
            _scenarioContext = scenarioContext;
            _homePage = new HomePage(driver);
            _ordersPage = new OrdersPage(driver);
            _newOrderPage = new NewOrderPage(driver);
        }

        [Given(@"I load the application and click on orders")]
        public void GivenILoadTheApplicationAndClickOnOrders()
        {
            _driverHelper?.Navigate(Url);
            Assert.That(_homePage?.IsHomePageLoaded() == true);
            _homePage?.ClickOrdersLink();
        }

        [When(@"I click on create new")]
        public void WhenIClickOnCreateNew()
        {
            Assert.That(_ordersPage?.IsOrderPageLoaded() == true);
            _ordersPage?.ClickCreateNewOrder();
        }

        [When(@"I enter a value for MRN")]
        public void WhenIEnterAValueForMRN()
        {
            _newOrderPage?.EnterMRN(_commonMethods.GenerateRandomString(5));
        }

        [When(@"I enter a value for all fields")]
        public void WhenIEnterAValueForAllFields()
        {
            _newOrderPage?.EnterMRN(_commonMethods.GenerateRandomString(5));
            _newOrderPage?.EnterFirstName(_commonMethods.GenerateRandomString(5));
            _newOrderPage?.EnterLastName(_commonMethods.GenerateRandomString(5));
            var accessionNum = _commonMethods.GenerateRandomString(5);
            _scenarioContext?.Set<string>(accessionNum, "AccessionNum");
            _newOrderPage?.EnterAccessionNum(accessionNum);
            _newOrderPage?.SelectOrganisation("Care UK (CUK)");
            _newOrderPage?.SelectSiteId("Sussex");
            _newOrderPage?.SelectModality("Xray (XR)");
            _newOrderPage?.EnterStudyDateTime(_commonMethods.GenerateCurrentDateTime("dd/MM/yyyy hh:mm tt"));
        }

        [When(@"I enter a value for all fields with existing MRN")]
        public void WhenIEnterAValueForAllFieldsWithExistingMRN()
        {
            FillExistingDetails(_commonMethods.GenerateRandomString(5));
        }

        [When(@"I enter a value for all fields with existing MRN & accession number")]
        public void WhenIEnterAValueForAllFieldsWithExistingMRNAccessionNumber()
        {
            FillExistingDetails("00487");
        }

        [When(@"I click on submit")]
        public void WhenIClickOnSubmit()
        {
            _newOrderPage?.ClickSubmit();
        }

        [When(@"I click on Cancel")]
        public void WhenIClickOnCancel()
        {
            _newOrderPage?.ClickCancel();
        }
        
        [Then(@"validation errors are displayed for all fields")]
        public void ThenValidationErrorsAreDisplayedForAllFields()
        {
            var result = _newOrderPage?.ErrorMessageDisplayed() == 7;
            result = _newOrderPage?.IsErrorMessagePresent("MRN is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("First Name is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("Last Name is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("Accession Number is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("Organisation is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("Site is required.") == true;
            result = _newOrderPage?.IsErrorMessagePresent("Study DateTime is required.") == true;
            Assert.That(result, Is.True);
        }

        [Then(@"validation errors are displayed for all fields except mrn")]
        public void ThenValidationErrorsAreDisplayedForAllFieldsExceptMrn()
        {
            var result = _newOrderPage?.ErrorMessageDisplayed() == 6;
            result = _newOrderPage?.IsErrorMessagePresent("MRN is required.") == false;
            Assert.That(result, Is.True);
        }

        [Then(@"validation errors are not displayed for any fields")]
        public void ThenValidationErrorsAreNotDisplayedForAnyFields()
        {
            Assert.That(_newOrderPage?.ErrorMessageDisplayed() == 0);
        }

        [When(@"the user is redirected to orders page")]
        [Then(@"the user is redirected to orders page")]
        public void ThenTheUserIsRedirectedToOrdersPage()
        {
            var result = _newOrderPage?.NewOrderIsSubmittedSuccessfully() == true;
            result = _ordersPage?.IsOrderPageLoaded() == true;
            Assert.That(result, Is.True);
        }

        [Then(@"the order is successfully created")]
        public void ThenTheOrderIsSuccessfullyCreated()
        {
            var orgCode = _ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"), "Org Code");
            _scenarioContext?.Set<string?>(
                _ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"), "Status"), "Status");
            Assert.That(orgCode=="CUK");
            _ordersPage?.DeleteOrder(_scenarioContext?.Get<string>("AccessionNum"));
        }

        [Then(@"the status is automatically set as SC")]
        public void ThenTheStatusIsAutomaticallySetAsSc()
        {
            Assert.That(_scenarioContext.Get<string>("Status").Equals("SC"));
        }


        [Then(@"no order is created")]
        public void ThenNoOrderIsCreated()
        {
            var orgCode = _ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"), "Org Code");
            Assert.That(string.IsNullOrEmpty(orgCode));
        }

        [Then(@"the order is successfully created with both the details")]
        public void ThenTheOrderIsSuccessfullyCreatedWithBothTheDetails()
        {
            var newOrgCode = _ordersPage?.GetOrderDetails(_scenarioContext?.Get<string>("AccessionNum"), "Org Code");
            var existingOrgCode = _ordersPage?.GetOrderDetails("00487", "Org Code");
            Assert.That(newOrgCode.Equals(existingOrgCode));
            _ordersPage?.DeleteOrder(_scenarioContext?.Get<string>("AccessionNum"));
        }


        [Then(@"the validation error is displayed")]
        public void ThenTheValidationErrorIsDisplayed()
        {
            var errorcount = _newOrderPage.ErrorMessageDisplayed();
            var errorMessagePresent = _newOrderPage.IsErrorMessagePresent("An order already exists with accession number [00487]");
            Assert.That(errorcount == 1 && errorMessagePresent == true);
        }

        private void FillExistingDetails(string accessionNum)
        {
            _newOrderPage?.EnterMRN("P303");
            _newOrderPage?.EnterFirstName("Sarah");
            _newOrderPage?.EnterLastName("Jones");
            _scenarioContext?.Set<string>(accessionNum, "AccessionNum");
            _newOrderPage?.EnterAccessionNum(accessionNum);
            _newOrderPage?.SelectOrganisation("Lumus (LUM)");
            _newOrderPage?.SelectSiteId("Baulkham Hills");
            _newOrderPage?.SelectModality("Ultrasound (US)");
            _newOrderPage?.EnterStudyDateTime(_commonMethods.GenerateCurrentDateTime("dd/MM/yyyy hh:mm tt"));
        }
    }
}
