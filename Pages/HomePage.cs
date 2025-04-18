namespace UIAutomation_Playwright.Pages
{
    public class HomePage : PageTest
    {
        private readonly ILocator _loggedUserName;
        private readonly IPage _page;

        public HomePage(IPage page)
        {
            _page = page;
            _loggedUserName = page.Locator(".logged-user-name");


        }

        public async Task loggedUserNameDisplayed()
        {
            await Expect(_loggedUserName).ToBeVisibleAsync();
        }
    }
}
