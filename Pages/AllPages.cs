
namespace UIAutomation_Playwright.Pages
{
    public sealed class AllPages
    {
        public HomePage hpage = null;
        public SamplePage spage = null;
        public ExtentReports _extent;
        public ExtentTest _test;
        public BrowserInitialise _browserInitialise;
        private AllPages()
        {
            
        }

        private static readonly Lazy<AllPages> instance = new Lazy<AllPages>(() => new AllPages());


        public static AllPages GetInstance()
        {
            return instance.Value;
        }

        public void InitialisePages()
        {
            spage = new SamplePage(_browserInitialise.Page);
            hpage = new HomePage(_browserInitialise.Page);
        }
    }
}
