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
public class GetDateTimeReportUnitTests
{
    [TestMethod]
    public void Account_Is_Not_Exist()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "4ucmqplxb028";

        var response = new
        {
            errorCode = (int)GameApiClientError.OverReportQueryLimit,
            errorMessage = "此功能仅能查询一天内的报表，您已超过上限",
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

        var startTime = DateTime.Parse("2022-06-20T01:30:00.000Z");
        var endTime = DateTime.Parse("2022-06-20T01:35:00.000Z");

        var request = new GetDateTimeReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            StartTime = startTime,
            EndTime = endTime,
            DataType = null,
            TimeType = null,
            GameNo1 = null
        };

        _ = Assert.ThrowsExceptionAsync<Exception>(async () => await client.GetDateTimeReportAsync(apiUrl, request));
    }

    [TestMethod]
    public async Task An_Interval_Of_10_Seconds_Is_Required_If_No_Data_Is_Found()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "4ucmqplxb028";

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

        var startTime = DateTime.Parse("2022-06-20T01:30:00.000Z");
        var endTime = DateTime.Parse("2022-06-20T01:35:00.000Z");

        var request = new GetDateTimeReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            StartTime = startTime,
            EndTime = endTime,
            DataType = null,
            TimeType = null,
            GameNo1 = null
        };

        var result = await client.GetDateTimeReportAsync(apiUrl, request);

        Assert.IsNotNull(result);
        Assert.IsFalse(result.Result.Any());
    }

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
                    user = "wmtf_4ucmqplxb029_GPI",
                    betId = "179229652",
                    betTime = "2022-06-17 10:30:16",
                    beforeCash = "570.0000",
                    bet = "50.0000",
                    validbet = "50.0000",
                    water = "0.0000",
                    result = "-50.0000",
                    betCode = "Dragon",
                    waterbet = "50.0000",
                    winLoss = "-50.0000",
                    ip = "220.132.206.70",
                    gid = "102",
                    @event = "113057686",
                    eventChild = "34",
                    round = "113057686",
                    subround = "34",
                    tableId = "31",
                    commission = "0",
                    settime = "2022-06-17 10:30:45",
                    reset = "N",
                    betResult ="龙",
                    gameResult = "龙:♦2 虎:♠5",
                    gname = "龙虎"
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

        var startTime = DateTime.Parse("2022-06-20T01:30:00.000Z");
        var endTime = DateTime.Parse("2022-06-20T01:35:00.000Z");

        var request = new GetDateTimeReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            StartTime = startTime,
            EndTime = endTime,
            DataType = null,
            TimeType = null,
            GameNo1 = null
        };
        var result = await client.GetDateTimeReportAsync(apiUrl, request);

        Assert.IsNotNull(result);
        Assert.AreEqual(response.result.First().round, result.Result.First().Round);
        Assert.AreEqual(response.result.First().subround, result.Result.First().SubRound);
    }

    [TestMethod]
    public void Timestamp_Is_Abnormal()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "4ucmqplxb028";

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

        var startTime = DateTime.Parse("2022-06-20T01:30:00.000Z");
        var endTime = DateTime.Parse("2022-06-20T01:35:00.000Z");

        var request = new GetDateTimeReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            StartTime = startTime,
            EndTime = endTime,
            DataType = null,
            TimeType = null,
            GameNo1 = null
        };

        _ = Assert.ThrowsExceptionAsync<Exception>(async () => await client.GetDateTimeReportAsync(apiUrl, request));
    }
}
