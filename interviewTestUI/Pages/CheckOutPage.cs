using interviewTestUI.Utils;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interviewTestUI.Pages
{
    public class CheckOutPage
    {
        private IPage _page;        
        public CheckOutPage(IPage page) => _page = page;

        //page Locators
        
        private ILocator firstName => _page.Locator("id=first-name");
        private ILocator lastName => _page.Locator("id=last-name");
        private ILocator postalCode => _page.Locator("id=postal-code");
        private ILocator continueButton => _page.Locator("id=continue");
        private ILocator finishButton => _page.Locator("id=finish");

        public async Task fillDetailsInCheckoutPageAndClickContinue()
        {
            string FirstName = RandomGenerator.GenerateRandomString(10);
            string LastName = RandomGenerator.GenerateRandomString(10);
            string PostalCode = RandomGenerator.GenerateRandomNumbers(6);

            //checkout
            await firstName.FillAsync(FirstName);
            await lastName.FillAsync(LastName);
            await postalCode.FillAsync(PostalCode);
            await continueButton.ClickAsync();
            await finishButton.ClickAsync();
        }
    }
}
