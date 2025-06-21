using EverlightRadiology.Framework.Wrapper;
using OpenQA.Selenium;
using static EverlightRadiology.Framework.Wrapper.TestConstant;

namespace EverlightRadiology.IntegrationTests.PageObjects
{
    internal class OrdersPage
    {
        private readonly WebHelper _webHelper;
        protected IWebElement? TxtOrderHeader =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                "#tableLabel");
        protected IWebElement? BtnOrder =>
            _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                "[class='btn btn-primary']");
        protected IWebElement? TableOrder =>
                    _webHelper.InitialiseDynamicWebElement(LocatorType.CssSelector,
                        "[class='table table-striped']");

        public OrdersPage(IWebDriver? driver) =>
            _webHelper = new WebHelper(driver);

        public bool IsOrderPageLoaded()
        {
            return getOrderPageHeader().Equals("Orders");
        }

        public void ClickCreateNewOrder() =>
            _webHelper.PerformWebDriverAction(BtnOrder, WebDriverAction.Click, null);

        public string? GetOrderDetails(string accessionNumber, string outputColumnName)
        {
            var webE = _webHelper.GetColumnWebElementFromWebTable(TableOrder, accessionNumber, outputColumnName);
            return _webHelper.ReturnVisibleText(webE);
        }

        public void DeleteOrder(string accessionNumber)
        {
            ClickXButton(accessionNumber);
            AcceptPopup();
        }

        public void ClickXButton(string accessionNumber)
        {
            var webE = _webHelper.GetColumnWebElementFromWebTable(TableOrder, accessionNumber, string.Empty);
            _webHelper.PerformWebDriverAction(webE, WebDriverAction.DoubleClick, null);
        }

        public void CancelPopup()
        {
            _webHelper.CancelPopup();
        }

        public void AcceptPopup()
        {
            _webHelper.ReadAndHandleAlert();
        }

        public void RefreshPage()
        {
            _webHelper.ReloadPage();
            _webHelper.GetPageReady();
        }

        private string? getOrderPageHeader() => _webHelper.ReturnVisibleText(TxtOrderHeader);
    }
}
