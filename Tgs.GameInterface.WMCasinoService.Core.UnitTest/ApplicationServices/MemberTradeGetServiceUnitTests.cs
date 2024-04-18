using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest.ApplicationServices;

[TestClass]
public class MemberTradeGetServiceUnitTests
{
	[TestMethod]
	public async Task Normal_Process()
	{
		var fakeClient = Substitute.For<IGameApiClient>();

		var sut = new MemberTradeGetService(fakeClient);

		var apiUrl = new Uri("http://localhost/");
		var vendorId = "testvendorid";
		var sign = "testsign";

		var query = new MemberTradeQuery(
			ApiUrl: apiUrl,
			VendorId: vendorId,
			Signature: sign,
			StartTime: null,
			EndTime: null,
			User: null,
			TransactionId: null,
			GameProviderTransactionId: null);

		var result = new GetMemberTradeReportResponse
		{
			ErrorCode = 0,
			ErrorMessage = "操作成功",
			Result =
			[
				new()
				{
					OrderId = "2147647425",
					AddTime = DateTimeOffset.Parse("2022-06-13 09:53:57"),
					Mid = "803799",
					Money = decimal.Parse("100.0000"),
					OpCode = "121",
					SubTotal = decimal.Parse("100.0000"),
					OrderNum = "",
					User = "4ucmqplxb029"
				},
				new()
				{
					OrderId = "2147647426",
					AddTime = DateTimeOffset.Parse("2022-06-13 09:55:10"),
					Mid = "803799",
					Money = decimal.Parse("-10.0000"),
					OpCode = "502",
					SubTotal = decimal.Parse("90.0000"),
					OrderNum = "101_113053285_5",
					User = "4ucmqplxb029"
				}
			]
		};

		_ = fakeClient.GetMemberTradeReportAsync(Arg.Any<Uri>(), Arg.Any<GetMemberTradeReportRequest>()).Returns(result);

		var actual = await sut.QueryAsync(query).ToArrayAsync();

		_ = await fakeClient
			.Received(1)
			.GetMemberTradeReportAsync(Arg.Any<Uri>(), Arg.Any<GetMemberTradeReportRequest>());

		Assert.IsNotNull(actual);
		Assert.AreEqual(2, actual.Length);
	}
}
