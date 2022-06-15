using Alberta.Data;
using Alberta.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alberta.Tests
{
    public class QuoteTests
    {
        //todo: mock the repo datasource, so then we can inject testing data collection
        //right now will execute against the hardcoded records

        [Fact]
        public void TestMissingParams()
        {

            var data = new PackageManager();
            var controller = new Controllers.PackagesController(data);
            var input = new QuoteRequest() { Destination = "", Packages = null, Source = "" };
            var result = controller.Quote(input);
            Assert.Equal(typeof(ActionResult<QuoteResponse>), result.GetType());
            Assert.Equal(typeof(BadRequestObjectResult), result.Result!.GetType());
            Assert.True(((result.Result as BadRequestObjectResult)?.Value as QuoteResponse)!.Message == "The request is incomplete, please validate");
        }

        [Fact]
        public void TestMissingRoutes()
        {
            var data = new PackageManager();
            var controller = new Controllers.PackagesController(data);
            var input = new QuoteRequest() { Destination = "WA", Packages = new List<Package>() { new Package() { Width = 1, Height = 1, Length = 1 } }, Source = "TN" };
            var result = controller.Quote(input);
            Assert.Equal(typeof(ActionResult<QuoteResponse>), result.GetType());
            Assert.Equal(typeof(NotFoundObjectResult), result.Result!.GetType());
            Assert.True(((result.Result as NotFoundObjectResult)?.Value as QuoteResponse)!.Message == "There aren't routes matching this request");
        }

        [Fact]
        public void TestSuccessful()
        {
            var data = new PackageManager();
            var controller = new Controllers.PackagesController(data);
            var input = new QuoteRequest() { Destination = "TX", Packages = new List<Package>() { new Package() { Width = 1, Height = 10, Length = 1 } }, Source = "TN" };
            var result = controller.Quote(input);
            Assert.Equal(typeof(ActionResult<QuoteResponse>), result.GetType());
            Assert.Equal(typeof(OkObjectResult), result.Result!.GetType());
            Assert.True(((result.Result as OkObjectResult)?.Value as QuoteResponse)!.Message == "Successful");
            Assert.Equal(200, ((result.Result as OkObjectResult)?.Value as QuoteResponse)!.Total);
        }
    }
}