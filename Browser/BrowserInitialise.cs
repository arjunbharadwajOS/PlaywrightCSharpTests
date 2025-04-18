namespace UIAutomation_Playwright.Browser
{
    public abstract class BrowserInitialise : IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;
        
        public BrowserInitialise()
        {
            _page = Task.Run(InitializePlaywright);
        }

        public IPage Page => _page.Result;

        public void Dispose()
        {
            _browser?.CloseAsync();
        }

        public abstract Task<IPage> InitializePlaywright();

    }
}
