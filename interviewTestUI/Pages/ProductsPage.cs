using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interviewTestUI.Pages
{
    public class ProductsPage
    {
        private IPage _page;
        public List<string>? itemsInProductPage;
        public ProductsPage(IPage page) => _page = page;

        //page Locators
        //private ILocator txtUsername => _page.Locator("id=user-name");
        //private ILocator txtPassword => _page.Locator("id=password");
        //private ILocator loginButton => _page.Locator("id=login-button");

        public async Task addItemsToCart()
        {
            var elements = await _page.QuerySelectorAllAsync("//*[@class='inventory_item_name ']");
            itemsInProductPage = new List<string>();
            foreach (var item in elements)
            {
                itemsInProductPage.Add(await item.InnerTextAsync());
            }
            
            var addToCart = await _page.QuerySelectorAllAsync("//button[text()='Add to cart']");
            
            foreach (var item in addToCart)
            {
                await item.ClickAsync();
            }
        }
    }
}
