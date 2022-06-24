using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.WMCasinoService.Core;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.Logging;

namespace Tgs.GameInterface.WMCasinoService.Repositories.UnitTest
{
    [TestClass]
    public class GetBalanceUnitTests
    {
        [TestMethod]
        public async Task Account_Is_Not_Exist()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var user = "4ucmqplxb028";

            var response = new
            {
                errorCode = (int)GameApiClientError.QueryNoUser,
                errorMessage = "查无此帐号,请检查",
                result = new decimal?()
            };

            var content = JsonSerializer.Serialize(response);
            var handler = new MockHttpMessageHandler();
            _ = handler
                .Expect(HttpMethod.Post, "")
                .Respond(HttpStatusCode.OK, new StringContent(content));

            var fakeHttpClient = handler.ToHttpClient();
            var fakeLog = NullLogger<GameApiClient>.Instance;
            var fakeTgsLog = Substitute.For<ITgsLog>();
            var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

            var request = new GetBalanceRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = user
            };
            var result = await client.GetBalanceAsync(apiUrl, request);

            Assert.IsFalse(result.IsPlayerExist);
            Assert.AreEqual(response.result, result.Balance);
        }

        [TestMethod]
        public async Task Normal_Process()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var user = "4ucmqplxb029";

            var response = new
            {
                errorCode = (int)GameApiClientError.Success,
                errorMessage = "操作成功",
                result = 520m
            };

            var content = JsonSerializer.Serialize(response);
            var handler = new MockHttpMessageHandler();
            _ = handler
                .Expect(HttpMethod.Post, "")
                .Respond(HttpStatusCode.OK, new StringContent(content));

            var fakeHttpClient = handler.ToHttpClient();
            var fakeLog = NullLogger<GameApiClient>.Instance;
            var fakeTgsLog = Substitute.For<ITgsLog>();
            var client = new GameApiClient(fakeHttpClient, fakeLog, fakeTgsLog);

            var request = new GetBalanceRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = user
            };
            var result = await client.GetBalanceAsync(apiUrl, request);

            Assert.IsTrue(result.IsPlayerExist);
            Assert.AreEqual(response.result, result.Balance);
        }
    }
}
