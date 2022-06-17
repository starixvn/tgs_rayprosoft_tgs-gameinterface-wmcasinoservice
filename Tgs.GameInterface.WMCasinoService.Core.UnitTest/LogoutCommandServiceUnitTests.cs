using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest
{
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
            var playerCode = "4ucmqplxb029";
            var command = new LogoutCommand(apiUrl, vendorId, sign, playerCode);

            await sut.ExecuteAsync(command);

            _ = await fakeClient
                .Received(1)
                .LogoutGameAsync(Arg.Any<Uri>(), Arg.Any<LogoutGameRequest>());
        }
    }
}
