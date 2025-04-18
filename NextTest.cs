
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Drawing;
using System.Threading.Tasks;
using BrowserType = Microsoft.Playwright.BrowserType;

namespace UIAutomation_Playwright
{
    [TestClass]
    public class NextTest
    {
        private BrowserInitialise _browserInitialise;
        public IBrowser browser = null;
        public IPage page = null;
        private static ExtentReports _extent;
        private static ExtentTest _test;

        [TestInitialize()]
        public void setUp()
        {
            var htmlReporter = new ExtentSparkReporter("PlaywrightTestReport.html");
            _extent = new ExtentReports();
            _extent.AttachReporter(htmlReporter);

            // Create a test case
            _test = _extent.CreateTest("Sample Playwright Test");

            Enum.TryParse(ApplicationConfiguration.GetSetting("MySetting:browser"), out GetBrowser currentBrowser);

            switch (currentBrowser)
            {
                case GetBrowser.Chrome:
                    _browserInitialise = new ChromeInitialise();
                    break;
                case GetBrowser.Firefox:
                    _browserInitialise = new FirefoxInitialise();
                    break;
                case GetBrowser.Webkit:
                    _browserInitialise = new WebkitInitialise();
                    break;
                default:
                    _browserInitialise = new ChromeInitialise();
                    break;
            }
        }


        [TestMethod]
        public async Task UITestMethod()
        {
            await _browserInitialise.Page.GotoAsync(url: "https://demo.applitools.com");

            await _browserInitialise.Page.ScreenshotAsync(new() { Path = "apitools.png" });

            SamplePage _samplePage = new SamplePage(_browserInitialise.Page);
            HomePage _homePage = new HomePage(_browserInitialise.Page);

            _samplePage.enterUserName("andy");
            _samplePage.enterPassword("i<3pandas");

            _samplePage.clickLogin();

            _homePage.loggedUserNameDisplayed();

            _test.Log(Status.Pass, "Title is correct: ");


        }

        [TestCleanup()]
        public async Task tearDown()
        {
            _browserInitialise.Dispose();
            _extent.Flush();
        }
    }
}
