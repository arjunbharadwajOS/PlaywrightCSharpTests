
namespace UIAutomation_Playwright
{
    [TestClass]
    public class NextTest
    {
        public IBrowser browser = null;
        public IPage page = null;
        public AllPages allPages;

        [TestInitialize()]
        public void setUp()
        {
            allPages = AllPages.GetInstance();
          
            var htmlReporter = new ExtentSparkReporter("ExecutionUIReport_"  + DateTime.Now.ToString("MM_dd_yy_hh_mm") + ".html");

            allPages._extent = new ExtentReports();
            allPages._extent.AttachReporter(htmlReporter);

            Enum.TryParse(ApplicationConfiguration.GetSetting("MySetting:browser"), out GetBrowser currentBrowser);

            switch (currentBrowser)
            {
                case GetBrowser.Chrome:
                    allPages._browserInitialise = new ChromeInitialise();
                    break;
                case GetBrowser.Firefox:
                    allPages._browserInitialise = new FirefoxInitialise();
                    break;
                case GetBrowser.Webkit:
                    allPages._browserInitialise = new WebkitInitialise();
                    break;
                default:
                    allPages._browserInitialise = new ChromeInitialise();
                    break;
            }
        }


        [TestMethod]
        public async Task UITestMethod()
        {
            // Create a test case
            allPages._test = allPages._extent.CreateTest(TestContext.TestName);
            allPages.InitialisePages();

            try
            {
                await allPages._browserInitialise.Page.GotoAsync(url: Data.URL.ToString());

                await allPages._browserInitialise.Page.ScreenshotAsync(new() { Path = "apitools.png" });

                allPages.spage.enterUserName("andy");
                allPages.spage.enterPassword("i<3pandas");

                allPages.spage.clickLogin();

                allPages.hpage.loggedUserNameDisplayed();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        [TestCleanup()]
        public async Task tearDown()
        {
            allPages._browserInitialise.Dispose();
            allPages._extent.Flush();
        }

        public TestContext TestContext { get; set; }
    }
}
