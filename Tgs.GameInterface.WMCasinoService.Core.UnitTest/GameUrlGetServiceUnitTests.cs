using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest;

[TestClass]
public class GameUrlGetServiceUnitTests
{
    [TestMethod]
    public async Task Normal_Process()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "wmtf_4ucmqplxb029_GPI";

        var query = new GameUrlQuery(
            ApiUrl: apiUrl,
            VendorId: vendorId,
            Signature: sign,
            User: user,
            Password: "password",
            Language: 0);

        var fakeClient = Substitute.For<IGameApiClient>();

        var request = new SigninGameRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            Password = "password",
            Lang = 0,
        };

        var gameUrl = "https://1rdhx.wmetg.com?sid=997308712I7G80019A168M7&ui=4&lang=cn&co=wm&stream=hls";
        var result = new SigninGameResponse
        {
            ErrorCode = 0,
            Message = "操作成功",
            GameUrl = gameUrl
        };

        _ = fakeClient
            .SigninGameAsync(apiUrl: Arg.Is(apiUrl), Arg.Is(request))
            .Returns(result);

        var service = new GameUrlGetService(fakeClient);

        var actual = await service.GetAsync(query).ConfigureAwait(false);

        Assert.AreEqual(gameUrl, actual.GameUrl);
    }
}
