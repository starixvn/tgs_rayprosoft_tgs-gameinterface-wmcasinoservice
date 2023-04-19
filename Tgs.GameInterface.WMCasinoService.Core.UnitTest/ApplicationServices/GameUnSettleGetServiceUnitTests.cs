using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest.ApplicationServices;

[TestClass]
public class GameUnSettleGetServiceUnitTests
{
    [TestMethod]
    public async Task Normal_Process()
    {
        var fakeClient = Substitute.For<IGameApiClient>();

        var sut = new GameUnSettleGetService(fakeClient);

        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var time = new DateTime(2022, 6, 17);

        var query = new GameUnSettleQuery(apiUrl, vendorId, sign, time);

        var request = new GetUnsettleReportRequest
        {
            VendorId = vendorId,
            Signature = sign,
            Time = time
        };

        var result = new GetUnsettleReportResponse
        {
            ErrorCode = 0,
            ErrorMessage = "操作成功",
            Result = new GetUnsettleReportResult[]
            {
                new GetUnsettleReportResult
                {
                    User = "4ucmqplxb029",
                    BetId = "179229337",
                    Gid = "101",
                    EventChild = "50",
                    BetTime = DateTimeOffset.Parse("2022-06-15 15:46:56"),
                    BetResult = "和",
                    Bet = decimal.Parse("10.0000"),
                    TableId = "201",
                    Commission = "0",
                    Round = "113055702",
                    SubRound = "50",
                    Gname = "百家乐",
                    AdditionalData="{\"betId\":\"179229337\"}"
                }
            }
        };

        _ = fakeClient
            .GetUnsettleReportAsync(Arg.Is(apiUrl), Arg.Is(request))
            .Returns(result);

        var actual = await sut.QueryAsync(query).ToArrayAsync();

        _ = await fakeClient
            .Received(1)
            .GetUnsettleReportAsync(Arg.Is(apiUrl), Arg.Is(request));

        Assert.IsNotNull(actual);
        Assert.AreEqual(1, actual.Length);
        Assert.AreEqual(result.Result.Single().AdditionalData, actual.Single().AdditionalData);
    }
}
