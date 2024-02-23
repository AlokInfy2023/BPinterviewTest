using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interviewTestUI.Pages
{
    public class LoginPage
    {
        private IPage _page;
        public LoginPage(IPage page) => _page = page;

        //page Locators
        private ILocator txtUsername => _page.Locator("id=user-name");
        private ILocator txtPassword => _page.Locator("id=password");
        private ILocator loginButton => _page.Locator("id=login-button");        

        public async Task login(string userName, string password)
        {
            await txtUsername.FillAsync(userName);
            await txtPassword.FillAsync(password);
            await loginButton.ClickAsync();            
        }
    }
}
