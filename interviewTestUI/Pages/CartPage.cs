using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interviewTestUI.Pages
{
    public class CartPage
    {
        private IPage _page;
        public List<string>? itemsInCartPage;
        public CartPage(IPage page) => _page = page;

        //page Locators
        private ILocator cartLink => _page.Locator("//*[@class='shopping_cart_link']");
        private ILocator checkoutButton => _page.Locator("id=checkout");
        //private ILocator loginButton => _page.Locator("id=login-button");

        public async Task checkItemsInCart()
        {
            //go to Cart
            await cartLink.ClickAsync();

            //Get the items in the cart in to  List
            var elements = await _page.QuerySelectorAllAsync("//*[@class='inventory_item_name']");
            itemsInCartPage = new List<string>();
            foreach (var item in elements)
            {
                itemsInCartPage.Add(await item.InnerTextAsync());
            }
        }
        public async Task ClickCheckoutButton()
        {
            await checkoutButton.ClickAsync();
        }
    }
}
