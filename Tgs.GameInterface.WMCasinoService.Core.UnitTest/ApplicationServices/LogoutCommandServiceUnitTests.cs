using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest.ApplicationServices;

[TestClass]
public class LogoutCommandServiceUnitTests
{
    [TestMethod]
    public async Task Normal_Process()
    {
        var fakeClient = Substitute.For<IGameApiClient>();

        var sut = new LogoutCommandService(fakeClient);

        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "wmtf_4ucmqplxb029_GPI";
        var command = new LogoutCommand(apiUrl, vendorId, sign, user);

        await sut.ExecuteAsync(command);

        _ = await fakeClient
            .Received(1)
            .LogoutGameAsync(Arg.Any<Uri>(), Arg.Any<LogoutGameRequest>());
    }
}
