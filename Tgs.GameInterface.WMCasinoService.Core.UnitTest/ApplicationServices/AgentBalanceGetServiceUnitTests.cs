using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest.ApplicationServices;

[TestClass]
public class AgentBalanceGetServiceUnitTests
{
	[TestMethod]
	public async Task Normal_Process()
	{
		var fakeClient = Substitute.For<IGameApiClient>();

		var sut = new AgentBalanceGetService(fakeClient);

		var apiUrl = new Uri("http://localhost/");
		var vedorId = "testVendorId";
		var sign = "testsign";

		var query = new AgentBalanceQuery(apiUrl, vedorId, sign);

		var request = new GetAgentBalanceRequest
		{
			VendorId = vedorId,
			Signature = sign
		};

		var result = new GetAgentBalanceResponse
		{
			ErrorCode = 0,
			Message = "操作成功",
			Balance = 168m
		};

		_ = fakeClient.GetAgentBalanceAsync(Arg.Is(apiUrl), Arg.Is(request))
			.Returns(result);

		var actual = await sut.GetAsync(query).ConfigureAwait(false);

		Assert.AreEqual(168m, actual.Balance);
	}
}
