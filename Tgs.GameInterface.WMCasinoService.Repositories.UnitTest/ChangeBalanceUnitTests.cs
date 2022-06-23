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
    public class ChangeBalanceUnitTests
    {
        [TestMethod]
        public void Account_Is_Not_Exist()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var playerCode = "4ucmqplxb028";

            var response = new
            {
                errorCode = (int)GameApiClientError.QueryNoUser,
                errorMessage = "查无此帐号,请检查",
                result = default(Nullable)
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

            var request = new ChangeBalanceRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = playerCode
            };

            _ = Assert.ThrowsExceptionAsync<Exception>(async () => await client.ChangeBalanceAsync(apiUrl, request));
        }

        [TestMethod]
        public async Task Normal_Process()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var playerCode = "4ucmqplxb029";

            var response = new
            {
                errorCode = (int)GameApiClientError.Success,
                errorMessage = "操作成功",
                result = new
                {
                    yourOrderNum = "42B69762-9471-E8C5-4BD7-4B8700D07049",
                    orderId = "2147648311",
                    cash = 620m
                }
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

            var request = new ChangeBalanceRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = playerCode
            };
            var result = await client.ChangeBalanceAsync(apiUrl, request);

            Assert.AreEqual(response.result.cash, result.CurrentBalance);
        }
    }
}
