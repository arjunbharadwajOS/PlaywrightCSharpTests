namespace UIAutomation_Playwright.Pages
{
    public class SamplePage
    {
        private readonly ILocator _txtPassword;
        private readonly ILocator _txtUserName;
        private readonly ILocator _clickLogin;
        private readonly IPage _page;

        public SamplePage(IPage page)
        {
            _page = page;
            _txtUserName = _page.Locator("#username");
            _txtPassword = _page.Locator("#password");
            _clickLogin = _page.Locator("#log-in");

        }

        public async Task enterUserName(string userName)
        {
            await _txtUserName.FillAsync(userName);
            //_test.Log(Status.Pass, "Title is correct: ");
        }

        public async Task enterPassword(string password)
        {
            await _txtPassword.FillAsync(password);
            //_test.Log(Status.Pass, "Title is correct: ");
        }

        public async Task clickLogin()
        {
            await _clickLogin.ClickAsync();
            //_test.Log(Status.Pass, "Title is correct: ");
        }
    }
}
