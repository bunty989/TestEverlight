using OpenQA.Selenium;
using EverlightRadiology.Framework.Wrapper;
using LocatorType = EverlightRadiology.Framework.Wrapper.
    TestConstant.LocatorType;
using WebDriverAction = EverlightRadiology.Framework.Wrapper.
    TestConstant.WebDriverAction;

namespace EverlightRadiology.IntegrationTests.PageObjects
{
    internal class HomePage
    {
        private readonly WebHelper _webHelper;
        protected IWebElement? TxtAppHeader =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                "app-home h2");
        protected IWebElement? LinkOrder =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                "[ng-reflect-router-link='/orders']");

        public HomePage(IWebDriver? driver) =>
            _webHelper = new WebHelper(driver);

        public bool IsHomePageLoaded()
        {
            return getHomePageHeader().Equals("Automation Test Sample Application");
        }

        public void ClickOrdersLink() =>
            _webHelper.PerformWebDriverAction(LinkOrder, WebDriverAction.Click, null);

        private string getHomePageHeader() => _webHelper.ReturnVisibleText(TxtAppHeader);
    }
}