using FluentAssertions;
using Microsoft.Playwright;
using Newtonsoft.Json;

namespace interviewTestAPI
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetRequestMultipleTimesAndAssertResponse()
        {
            List<string> messageValues = new List<string>(); 
            var playwright = await Playwright.CreateAsync();

            for (int i = 0; i < 3; i++)
            {
                var requestContext = await playwright.APIRequest.NewContextAsync(new()
                {
                    BaseURL = "https://dog.ceo/"
                });

                var response = await requestContext.GetAsync("api/breed/hound/images/random");
                var data = await response.TextAsync();
                
                var image =JsonConvert.DeserializeObject<ResponseMessage>(data);
                var message = image.Message;
                var status = image.Status;
                messageValues.Add(message);
                status.Should().Be("success");
            }

            bool areAllElementsUnique = messageValues.Distinct().Count() == messageValues.Count;
            areAllElementsUnique.Should().BeTrue();
        }
    }

    public class ResponseMessage
    {
        public string? Message { get; set;}
        public string? Status { get; set; }
    }

}