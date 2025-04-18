namespace UIAutomation_Playwright
{
    [TestClass]
    public sealed class UITest : PageTest
    {
        private const string urlString = "http://eaapp.somee.com/";

        [TestMethod]
        public async Task PlayTestMethod()
        {
            IPage page = await Context.NewPageAsync();
            await page.GotoAsync(urlString);

            await page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();

            await page.GetByLabel("UserName").ClickAsync();

            await page.GetByLabel("UserName").FillAsync("admin");

            await page.GetByLabel("UserName").PressAsync("Tab");

            await page.GetByLabel("Password").FillAsync("password");

            await page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Employee List" }).ClickAsync();

            await page.GetByRole(AriaRole.Link, new() { Name = "Create New" }).ClickAsync();

            await Expect(page.GetByRole(AriaRole.Heading, new() { Name = "Employee" })).ToBeVisibleAsync();


        }

        
    
    }
}
