using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichardSzalay.MockHttp;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest;

[TestClass]
public class SigninGameUnitTests
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
			result = "https://1rdhx.wmetg.com?sid=9973012G8C90016A168M785&ui=4&lang=cn&co=wm&stream=hls"
		};

		var content = JsonSerializer.Serialize(response);
		var handler = new MockHttpMessageHandler();
		_ = handler
			.Expect(HttpMethod.Post, "")
			.Respond(HttpStatusCode.OK, new StringContent(content));

		var fakeHttpClient = handler.ToHttpClient();
		var fakeLog = NullLogger<GameApiClient>.Instance;
		var client = new GameApiClient(fakeLog, fakeHttpClient);

		var request = new SigninGameRequest
		{
			VendorId = vendorId,
			Signature = sign,
			User = user,
			Password = "password",
			Lang = 0
		};
		var result = await client.SigninGameAsync(apiUrl, request);

		Assert.AreEqual(response.errorCode, result.ErrorCode);
		Assert.AreEqual(response.errorMessage, result.Message);
		Assert.AreEqual(response.result, result.GameUrl);
	}
}
