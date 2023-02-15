using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest;

[TestClass]
public class CreatePlayerCommandServiceUnitTests
{
    [TestMethod]
    public async Task Normal_Process()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "wmtf_4ucmqplxb029_GPI";
        var userName = "wmtf_4ucmqplxb029_GPI";
        var password = "password";

        var query = new CreatePlayerCommand(
            ApiUrl: apiUrl,
            VendorId: vendorId,
            Signature: sign,
            User: user,
            Password: password,
            Username: userName);

        var fakeClient = Substitute.For<IGameApiClient>();

        var request = new MemberRegisterRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            Username = userName,
            Password = password
        };

        var sut = new CreatePlayerCommandService(fakeClient);

        await sut.ExecuteAsync(query).ConfigureAwait(false);

        _ = await fakeClient
            .Received(1)
            .MemberRegisterAsync(apiUrl: Arg.Is(apiUrl), Arg.Is(request));
    }
}
