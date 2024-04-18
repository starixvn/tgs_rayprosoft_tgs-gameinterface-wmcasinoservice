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
public class GetAgentBalanceUnitTests
{
	[TestMethod]
	public async Task Normal_Process()
	{
		var apiUrl = new Uri("http://localhost/");
		var vendorId = "testvendorid";
		var sign = "testsign";

		var response = new
		{
			errorCode = (int)GameApiClientError.Success,
			errorMessage = "操作成功",
			result = 17621.5m
		};

		var content = JsonSerializer.Serialize(response);
		var handler = new MockHttpMessageHandler();
		_ = handler
			.Expect(HttpMethod.Post, "")
			.Respond(HttpStatusCode.OK, new StringContent(content));

		var fakeHttpClient = handler.ToHttpClient();
		var fakeLog = NullLogger<GameApiClient>.Instance;
		var client = new GameApiClient(fakeLog, fakeHttpClient);

		var request = new GetAgentBalanceRequest
		{
			VendorId = vendorId,
			Signature = sign
		};
		var result = await client.GetAgentBalanceAsync(apiUrl, request);

		Assert.AreEqual(response.result, result.Balance);
	}
}
