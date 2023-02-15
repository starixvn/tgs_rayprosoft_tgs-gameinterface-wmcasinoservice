using System;
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

namespace Tgs.GameInterface.WMCasinoService.Repositories.UnitTest;

[TestClass]
public class MemberRegisterUnitTests
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
            result = "新增完成"
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

        var request = new MemberRegisterRequest
        {
            VendorId = vendorId,
            Signature = sign,
            User = user,
            Password = "password",
            Username = user
        };
        var result = await client.MemberRegisterAsync(apiUrl, request);

        Assert.AreEqual(response.errorCode, result.ErrorCode);
    }
}
