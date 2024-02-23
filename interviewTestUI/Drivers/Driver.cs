using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace interviewTestUI.Drivers
{
    public class Driver:IDisposable
    {
        private readonly Task<IPage> _page;
        private IBrowser? _browser;

        public Driver() => _page = initializePlaywright();

        public IPage Page => _page.Result;

        public async Task<IPage> initializePlaywright()
        {
            //Playwright
            var playwright = await Playwright.CreateAsync();

            //Browser
            string? Browser = ConfigurationManager.AppSettings["Browser"]?.ToString().ToLower();
            switch (Browser)
            {
                case "chromium":
                    _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });
                    break;
                case "firefox":
                    _browser = await playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });
                    break;
                case "webkit":
                    _browser = await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });
                    break;
                default:
                    _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                    {
                        Headless = false
                    });
                    break;                    
            }
            var context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = null
            });

            //Page
            return await _browser.NewPageAsync();
        }
        public void Dispose() => _browser?.CloseAsync();
    }
}
