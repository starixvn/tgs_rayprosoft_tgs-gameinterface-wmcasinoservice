using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.WMCasinoService.Core;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Repositories.UnitTest;

[TestClass]
public class GetUnsettleReportUnitTests
{
    [TestMethod]
    public async Task An_Interval_Of_10_Seconds_Is_Required_If_No_Data_Is_Found()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";

        var response = new
        {
            errorCode = (int)GameApiClientError.TryAfter10Sec,
            errorMessage = "10秒內禁止重複操作",
            result = default(Nullable)
        };

        var content = JsonSerializer.Serialize(response);
        var handler = new MockHttpMessageHandler();
        _ = handler
            .Expect(HttpMethod.Post, "")
            .Respond(HttpStatusCode.OK, new StringContent(content));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeLog, fakeHttpClient);

        var request = new GetUnsettleReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            Time = new DateTime(2022, 6, 19, 0, 0, 0)
        };

        var result = await client.GetUnsettleReportAsync(apiUrl, request);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Result.Length != 0);
    }

    [TestMethod]
    public async Task Normal_Process()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";

        var response = new
        {
            errorCode = (int)GameApiClientError.Success,
            errorMessage = "操作成功",
            result = new[]
            {
                new
                {
                    user = "4ucmqplxb029",
                    betId="179229908",
                    gid="108",
                    @event="113060931",
                    eventChild="97",
                    betTime="2022-06-20 09:37:25",
                    betResult="小",
                    bet="20.0000",
                    tableId="1",
                    commission="0",
                    round = "113060931",
                    subround ="97",
                    gname ="色碟"
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
        var client = new GameApiClient(fakeLog, fakeHttpClient);

        var request = new GetUnsettleReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            Time = new DateTime(2022, 6, 19, 0, 0, 0)
        };
        var result = await client.GetUnsettleReportAsync(apiUrl, request);

        Assert.AreEqual(response.result.First().round, result.Result.First().Round);
        Assert.AreEqual(response.result.First().subround, result.Result.First().SubRound);
    }

    [TestMethod]
    public void Timestamp_Is_Abnormal()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";

        var response = new
        {
            errorCode = (int)GameApiClientError.UnusualTimestamp,
            errorMessage = "Timestamp is abnormal, more than 30 seconds",
            result = default(Nullable)
        };

        var content = JsonSerializer.Serialize(response);
        var handler = new MockHttpMessageHandler();
        _ = handler
            .Expect(HttpMethod.Post, "")
            .Respond(HttpStatusCode.OK, new StringContent(content));

        var fakeHttpClient = handler.ToHttpClient();
        var fakeLog = NullLogger<GameApiClient>.Instance;
        var client = new GameApiClient(fakeLog, fakeHttpClient);

        var request = new GetUnsettleReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            Time = new DateTime(2022, 6, 19, 0, 0, 0)
        };

        _ = Assert.ThrowsExceptionAsync<Exception>(async () => await client.GetUnsettleReportAsync(apiUrl, request));
    }
}
