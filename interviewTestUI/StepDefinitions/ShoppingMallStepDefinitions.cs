using interviewTestUI.Drivers;
using interviewTestUI.Pages;
using Microsoft.Playwright;
using System.IO;
using TechTalk.SpecFlow.Assist;

namespace interviewTestUI.StepDefinitions
{
    [Binding]
    public sealed class ShoppingMallStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly Driver _driver;
        private readonly LoginPage _loginPage;
        private readonly ProductsPage _productsPage;
        private readonly CartPage _cartPage;
        private readonly CheckOutPage _checkoutPage;

        public string screenshotPath = Directory.GetParent(@"../../../")?.FullName + Path.DirectorySeparatorChar + "Screenshots" 
            + Path.DirectorySeparatorChar;
        
        public ShoppingMallStepDefinitions(ScenarioContext scenarioContext, Driver driver)
        {
            _scenarioContext = scenarioContext;
            _driver = driver;
            _loginPage = new LoginPage(_driver.Page);
            _productsPage = new ProductsPage(_driver.Page);
            _cartPage = new CartPage(_driver.Page);
            _checkoutPage = new CheckOutPage(_driver.Page);
        }

        [Given(@"the user navigtes to the shopping website ""([^""]*)""")]
        public void GivenTheUserNavigtesToTheShoppingWebsite(string URL)
        {
            _driver.Page.GotoAsync(URL);            
        }

        [Given(@"the user logs in with the username and password")]
        public async Task GivenTheUserLogsInWithTheUsernameAndPassword(Table table)
        {
            dynamic data = table.CreateDynamicInstance();
            await _loginPage.login((string)data.userName, (string)data.password);
            _driver.Page.Url.Should().Contain("inventory");
        }

        [When(@"the user places all items into the cart")]
        public async Task WhenTheUserPlacesAllItemsIntoTheCart()
        {
            await _productsPage.addItemsToCart();
        }

        [Then(@"the user moves to the cart section to verify added products")]
        public async Task ThenTheUserMovesToTheCartSectionToVerifyAddedProducts()
        {
            //await _driver.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _cartPage.checkItemsInCart();
            _cartPage.itemsInCartPage.Should().Equal(_productsPage.itemsInProductPage);
            _cartPage.itemsInCartPage.Should().HaveSameCount(_productsPage.itemsInProductPage);
        }

        [Then(@"user finally checksout and verifies that order was successful")]
        public async Task ThenUserFinallyChecksoutAndVerifiesThatOrderWasSuccessful()
        {
            //await _driver.Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            await _cartPage.ClickCheckoutButton();
            await _checkoutPage.fillDetailsInCheckoutPageAndClickContinue();
            _driver.Page.Url.Should().Contain("checkout-complete");
            if (!Directory.Exists(screenshotPath))
            {
                Directory.CreateDirectory(screenshotPath);
            }

            string fileName = DateTime.Now.ToString("ddMMyyyy HHmmss") + "_screenshot.png";
            await _driver.Page.ScreenshotAsync(new() { Path = screenshotPath + fileName });

        }

    }

}
