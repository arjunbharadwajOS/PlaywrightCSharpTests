namespace UIAutomation_Playwright.Browser
{
    public class ChromeInitialise : BrowserInitialise
    {
        private IBrowser? _browser;
        private readonly Task<IPage> _page;

        public ChromeInitialise() : base() {
            _page = Task.Run(InitializePlaywright);
        }

        public override async Task<IPage> InitializePlaywright()
        {
            var playwright = await Playwright.CreateAsync();

            //Browser
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = false
            });
            //Page
            return await _browser.NewPageAsync();
        }

    }
}
