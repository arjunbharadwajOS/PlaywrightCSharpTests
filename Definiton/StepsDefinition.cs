using AventStack.ExtentReports.Gherkin.Model;
using Reqnroll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIAutomation_Playwright.Pages;

namespace UIAutomation_Playwright.Definiton
{
    [Binding]
    public class StepsDefinition
    {
        public IBrowser browser = null;
        public IPage page = null;
        public AllPages allPages;
        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static ExtentReports extent;

        private readonly FeatureContext _featureContext;
        private readonly ScenarioContext _scenarioContext;

        public StepsDefinition(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            _featureContext = featureContext;
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void beforeScenario()
        {
            var htmlReporter = new ExtentSparkReporter("ExecutionSpecflowReport_" + DateTime.Now.ToString("MM_dd_yy_hh_mm") + ".html");

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);

            allPages = AllPages.GetInstance();

            featureName = extent.CreateTest<Feature>(_featureContext.FeatureInfo.Title);

            //Get scenario name
            scenario = featureName.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);

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

        [AfterStep]
        public void InsertReportingSteps()
        {
            var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();


            if (_scenarioContext.TestError == null)
            {
                if (stepType == "Given")
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "When")
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "Then")
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                else if (stepType == "And")
                    scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
            }
            else if (_scenarioContext.TestError != null)
            {
                if (stepType == "Given")
                {
                    scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);

                }
                else if (stepType == "When")
                {
                    scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.InnerException);

                }
                else if (stepType == "Then")
                {
                    scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text).Fail(_scenarioContext.TestError.Message);
                }
            }



        }

        [AfterScenario]
        public void afterScenario()
        {
            Console.WriteLine("after");
            allPages._browserInitialise.Dispose();
            extent.Flush();
        }

        [Given("I launch the site")]
        public void givenMethod()
        {
            allPages = AllPages.GetInstance();
            allPages.InitialisePages();

            try
            {
                allPages._browserInitialise.Page.GotoAsync(url: Data.URL.ToString());
                allPages._browserInitialise.Page.ScreenshotAsync(new() { Path = "apitools.png" });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [When("I view the page")]
        public void whenMethod()
        {
            allPages.InitialisePages();
            allPages.spage.enterUserName("andy");
            allPages.spage.enterPassword("i<3pandas");
            allPages.spage.clickLogin();
        }

        [Then("I check the results")]
        public void thenMethod()
        {
            allPages.InitialisePages();
            allPages.hpage.loggedUserNameDisplayed();
        }


    }
}
