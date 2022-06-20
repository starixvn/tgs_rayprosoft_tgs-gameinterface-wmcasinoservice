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
            var startTime = new DateTime(2022, 6, 17);

            var query = new GamePullRecordQuery(
                ApiUrl: apiUrl,
                VendorId: vendorId,
                Signature: sign,
                StartTime: startTime,
                EndTime: null,
                PlayerCode: null);

            var request = new GetDateTimeReportRequest
            {
                VendorId = vendorId,
                Signature = sign,
                StartTime = startTime,
                EndTime = null,
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
                        BetTime = "2022-06-17 10:30:16",
                        BeforeCash = "570.0000",
                        Bet = "50.0000",
                        Validbet = "50.0000",
                        Water = "0.0000",
                        Result = "-50.0000",
                        BetCode = "Dragon",
                        Waterbet = "50.0000",
                        WinLoss = "-50.0000",
                        Ip = "220.132.206.70",
                        Gid = "102",
                        Event = "113057686",
                        EventChild = "34",
                        Round = "113057686",
                        Subround = "34",
                        TableId = "31",
                        Commission = "0",
                        Settime = "2022-06-17 10:30:45",
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
