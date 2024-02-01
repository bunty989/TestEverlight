using TechTalk.SpecFlow;

namespace EverlightRadiology.Framework.Wrapper
{
    [Binding]
    public class ContextInjBase
    {
        protected readonly ApiContext ApiContext;

        public ContextInjBase(ApiContext apiContext)
        {
            ApiContext = apiContext;
        }
    }
}
