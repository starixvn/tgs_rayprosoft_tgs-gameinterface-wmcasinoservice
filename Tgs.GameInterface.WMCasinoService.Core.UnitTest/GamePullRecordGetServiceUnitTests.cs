using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest
{
    [TestClass]
    public class GamePullRecordGetServiceUnitTests
    {
        [TestMethod]
        public async Task Normal_Process()
        {
            var fakeClient = Substitute.For<IGameApiClient>();

            var sut = new GamePullRecordGetService(fakeClient);

            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var startTime = DateTime.Parse("2022-06-20T01:30:00.000Z");
            var endTime = DateTime.Parse("2022-06-20T01:35:00.000Z");

            var query = new GamePullRecordQuery(
                ApiUrl: apiUrl,
                VendorId: vendorId,
                Signature: sign,
                StartTime: startTime,
                EndTime: endTime);

            var request = new GetDateTimeReportRequest
            {
                VendorId = vendorId,
                Signature = sign,
                StartTime = startTime,
                EndTime = endTime,
                User = null,
                DataType = null,
                TimeType = null,
                GameNo1 = null
            };

            var result = new GetDateTimeReportResponse
            {
                ErrorCode = 0,
                ErrorMessage = "操作成功",
                Result = new GetDateTimeReportResult[]
                {
                    new GetDateTimeReportResult
                    {
                        User = "4ucmqplxb029",
                        BetId = "179229652",
                        BetTime = DateTimeOffset.Parse("2022-06-17 10:30:16"),
                        BeforeCash = decimal.Parse("570.0000"),
                        Bet = decimal.Parse("50.0000"),
                        Validbet = decimal.Parse("50.0000"),
                        Water = decimal.Parse("0.0000"),
                        Result = decimal.Parse("-50.0000"),
                        BetCode = "Dragon",
                        Waterbet = decimal.Parse("50.0000"),
                        WinLoss = decimal.Parse("-50.0000"),
                        Ip = "220.132.206.70",
                        Gid = "102",
                        Event = "113057686",
                        EventChild = "34",
                        Round = "113057686",
                        SubRound = "34",
                        TableId = "31",
                        Commission = "0",
                        Settime = DateTimeOffset.Parse("2022-06-17 10:30:45"),
                        Reset = "N",
                        BetResult = "龙",
                        GameResult = "龙:♦2 虎:♠5",
                        Gname = "龙虎"
                    }
                }
            };

            _ = fakeClient.GetDateTimeReportAsync(Arg.Is(apiUrl), Arg.Is(request)).Returns(result);

            var actual = await sut.QueryAsync(query).ToArrayAsync();

            _ = await fakeClient
                .Received(1)
                .GetDateTimeReportAsync(Arg.Is(apiUrl), Arg.Is(request));

            Assert.IsNotNull(actual);
            Assert.AreEqual(1, actual.Length);
        }
    }
}
