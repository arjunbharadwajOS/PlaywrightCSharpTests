using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomation_Playwright.Pages;

namespace UIAutomation_Playwright
{
    [TestClass]
    public class APITests : PlaywrightTest
    {
        private IAPIRequestContext Request = null!;
        public AllPages allPages;

        [TestInitialize]
        public async Task SetUpAPITesting()
        {
            //await CreateAPIRequestContext();
            allPages = AllPages.GetInstance();

            var htmlReporter = new ExtentSparkReporter("ExecutionAPIReport_" + DateTime.Now.ToString("MM_dd_yy_hh_mm") + ".html");

            allPages._extent = new ExtentReports();
            allPages._extent.AttachReporter(htmlReporter);
        }

        [TestMethod]
        public async Task CreateAPIRequestContext()
        {
            allPages._test = allPages._extent.CreateTest("APITest");
            var headers = new Dictionary<string, string>();
            // We set this header per GitHub guidelines.
            headers.Add("Accept", "application/json");
            // Add authorization token to all requests.
            // Assuming personal access token available in the environment.
            

            Request = await this.Playwright.APIRequest.NewContextAsync(new()
            {
                // All requests we send go to this API endpoint.
                BaseURL = "http://api.zippopotam.us/us/90210",
                ExtraHTTPHeaders = headers,
            });
        }

        [TestCleanup]
        public async Task TearDownAPITesting()
        {
            await Request.DisposeAsync();
            allPages._extent.Flush();
        }


    }
}
