using System;
using System.Linq;
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
    public class GetMemberTradeReportUnitTests
    {
        [TestMethod]
        public async Task Normal_Process()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var user = "wmtf_4ucmqplxb029_GPI";

            var response = new
            {
                errorCode = (int)GameApiClientError.Success,
                errorMessage = "操作成功",
                result = new[]
                {
                    new
                    {
                        orderid = "2147648319",
                        addtime = "2022-06-17 17:07:44",
                        mid = "803799",
                        money = "100.0000",
                        op_code = "121",
                        subtotal = "720.0000",
                        ordernum = "D18D0651-FDE1-6CDF-1717-57D44BAE",
                        user = "wmtf_4ucmqplxb029_GPI"
                    }
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

            var request = new GetMemberTradeReportRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = user,
                StartTime = new DateTime(2022, 6, 17, 16, 0, 0),
                EndTime = null,
                Order = null,
                OrderId = null
            };
            var result = await client.GetMemberTradeReportAsync(apiUrl, request);

            Assert.AreEqual(response.result.First().ordernum, result.Result.First().OrderNum);
        }

        [TestMethod]
        public async Task Query_Could_Be_Empty()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var user = "4ucmqplxb028";

            var response = new
            {
                errorCode = 107,
                errorMessage = "指令操作成功但查无此笔资料",
                result = Array.Empty<object>(),
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

            var request = new GetMemberTradeReportRequest
            {
                VendorId = vendorId,
                Signature = sign,
                User = user,
                StartTime = null,
                EndTime = null,
                Order = null,
                OrderId = null
            };
            var result = await client.GetMemberTradeReportAsync(apiUrl, request);

            Assert.AreEqual(response.result.Length, result.Result.Length);
        }
    }
}
