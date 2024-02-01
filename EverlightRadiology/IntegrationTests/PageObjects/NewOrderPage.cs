using EverlightRadiology.Framework.Wrapper;
using OpenQA.Selenium;
using System.Globalization;
using static EverlightRadiology.Framework.Wrapper.TestConstant;

namespace EverlightRadiology.IntegrationTests.PageObjects
{
    internal class NewOrderPage
    {
        private readonly WebHelper _webHelper;
        protected IWebElement? TxtOrderHeader =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                "#tableLabel");
        protected IWebElement? TxtBoxMRN =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#mrn");
        protected IWebElement? TxtBoxFirstName =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#first-name");
        protected IWebElement? TxtBoxLastName =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#last-name");
        protected IWebElement? TxtBoxAccessionNum =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#accession-number");
        protected IWebElement? DropDownOrganisation =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#org-code");
        protected IWebElement? DropdownSiteId =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#site-id");
        protected IWebElement? DropDownModality =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#modality");
        protected IWebElement? TxtBoxStudyDateTime =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "#study-date-time");
        protected IWebElement? BtnSubmit =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "[type='submit']");
        protected IWebElement? BtnCancel =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector, "[class='btn btn-warning']");
        protected List<IWebElement?> LabelErrorMessageCollection =>
            _webHelper.InitialiseWebElementsCollection(LocatorType.CssSelector, ".text-danger");

        public NewOrderPage(IWebDriver? driver) =>
            _webHelper = new WebHelper(driver);

        public void EnterMRN(string mrnValue)
        {
            _webHelper.PerformWebDriverAction(TxtBoxMRN, WebDriverAction.Input, mrnValue);
        }

        public void EnterFirstName(string firstName) =>
        _webHelper.PerformWebDriverAction(TxtBoxFirstName,WebDriverAction.Input, firstName);

        public void EnterLastName(string lastName) =>
        _webHelper.PerformWebDriverAction(TxtBoxLastName, WebDriverAction.Input, lastName);

       
        public void EnterAccessionNum(string accessionNum) =>
        _webHelper.PerformWebDriverAction(TxtBoxAccessionNum, WebDriverAction.Input, accessionNum);

        public void SelectOrganisation(string organisation) =>
        _webHelper.PerformWebDriverAction(DropDownOrganisation, WebDriverAction.Select, organisation);

        public void SelectSiteId(string siteId) =>
        _webHelper.PerformWebDriverAction(DropdownSiteId, WebDriverAction.Select, siteId);

        public void SelectModality(string modality) =>
            _webHelper.PerformWebDriverAction(DropDownModality, WebDriverAction.Select, modality);

        public void EnterStudyDateTime(string dateTime)
        {
            var cultureType = CultureInfo.InvariantCulture;
            var dtTime = DateTime.ParseExact(dateTime, "dd/MM/yyyy hh:mm tt", cultureType);
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.DoubleClick, null);
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                 dtTime.ToString("dd", cultureType));
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                 dtTime.ToString("MM", cultureType));
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                  dtTime.ToString("yyyy", cultureType));
            _webHelper.KeyboardAction(KeyBoardAction.Tab, 1);
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                 dtTime.ToString("hh", cultureType));
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                  dtTime.ToString("mm", cultureType));
            _webHelper.PerformWebDriverAction(TxtBoxStudyDateTime, WebDriverAction.SendKeys,
                 dtTime.ToString("tt", cultureType));
        }

        public void ClickSubmit() =>
        _webHelper.PerformWebDriverAction(BtnSubmit, WebDriverAction.Click, null);

        public void ClickCancel() =>
        _webHelper.PerformWebDriverAction(BtnCancel, WebDriverAction.Click, null);

        public int ErrorMessageDisplayed()
        {
            return LabelErrorMessageCollection.Count;
        }

        public bool IsErrorMessagePresent(String errorMessage)
        {
            var webE =  LabelErrorMessageCollection.SingleOrDefault(x =>
            _webHelper.ReturnVisibleText(x).Equals(errorMessage, StringComparison.Ordinal));
            return webE is IWebElement;
        }
    }
}
