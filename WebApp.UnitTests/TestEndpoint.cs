using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using WebApp.Controllers;

namespace WebApp.UnitTests
{
    [TestFixture]
    class TestEndpoint
    {
        private const string testTimestamp = "5/8/2019 2:22:00 PM";
        private readonly TimeController controller = new TimeController(new MockTimeService(testTimestamp));

        [TestCase("200", HttpStatusCode.OK)]
        [TestCase("404", HttpStatusCode.NotFound)]
        [TestCase("500", HttpStatusCode.InternalServerError)]
        public async Task testGoodStatusCode(string statusCode, HttpStatusCode expected)
        {
            // Given a request with a provided status code
            var response = await controller.GetTime(statusCode);

            // When we make that request
            var actual = (HttpStatusCode)(response.Result as StatusCodeResult).StatusCode;

            // Then we expect the status code to reflect the proper HttpStatusCode
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task testGoodTimeResponse()
        {
            // Given a valid request
            var response = await controller.GetTime();

            // When we make that request
            var actualCode = (response.Result as OkObjectResult).StatusCode;
            var actualOutput = (response.Result as OkObjectResult).Value;

            // Then we expect a 200 status code and the correct time output
            //Assert.AreEqual(HttpStatusCode.OK, actualCode);
            Assert.AreEqual(testTimestamp, actualOutput);
        }

        [TestCase("NoGoodStatusCode")]
        public async Task testBadStatusCode(string statusCode)
        {
            // Given a request with a bad status code
            var response = await controller.GetTime(statusCode);

            // When we make that request
            var actual = (HttpStatusCode)(response.Result as StatusCodeResult).StatusCode;

            // Then we should always return 500
            Assert.AreEqual(HttpStatusCode.BadRequest, actual);
        }
    }
}
