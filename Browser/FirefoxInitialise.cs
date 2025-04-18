namespace UIAutomation_Playwright.Browser
{
    public class FirefoxInitialise : BrowserInitialise
    {
        private IBrowser? _browser;
        private readonly Task<IPage> _page;

        public FirefoxInitialise() : base()
        {
            _page = Task.Run(InitializePlaywright);
        }

        public override async Task<IPage> InitializePlaywright()
        {
            var playwright = await Playwright.CreateAsync();

            //Browser
            _browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            //Page
            return await _browser.NewPageAsync();
        }
    }
   
}
