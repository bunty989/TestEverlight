using TechTalk.SpecFlow;

namespace EverlightRadiology.Framework.Wrapper
{
    [Binding]
    public class BaseApiSteps
    {
        // ReSharper disable once InconsistentNaming
        protected readonly ApiContext _apiContext;
        // ReSharper disable once NotAccessedField.Local
        private readonly ScenarioContext _scenarioContext;
        public BaseApiSteps(ScenarioContext scenarioContext, ApiContext apiContext)
        {
            _apiContext = apiContext;
            _scenarioContext = scenarioContext;
        }

        [When(@"I get a response message of: '(.*)'")]
        public void WhenIGetAResponseMessageOf(string message)
        {
            ApiBase.AssertMessageReturned(message, _apiContext.Response);
        }

        [Then(@"I expect a status code of: (.*)")]
        public void ThenIExpectAStatusCodeOf(int code)
        {
            ApiBase.AssertStatusCodeReturned(code, _apiContext.Response);
        }


        [Then(@"the value of node '([^']*)' is '([^']*)'")]
        public void ThenTheValueOfNodeIs(string nodeName, string nodeExpectedValue)
        {
            ApiBase.AssertNodeValue(nodeName, nodeExpectedValue);
        }
    }
}
