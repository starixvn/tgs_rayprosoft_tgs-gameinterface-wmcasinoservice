using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest;

[TestClass]
public class FundOutGetServiceUnitTests
{
    [TestMethod]
    public async Task Normal_Process()
    {
        var apiUrl = new Uri("http://localhost/");
        var vendorId = "testvendorid";
        var sign = "testsign";
        var user = "wmtf_4ucmqplxb029_GPI";
        string transactionId = Guid.NewGuid().ToString("N");
        decimal amount = 100m;

        var query = new FundOutQuery(
            ApiUrl: apiUrl,
            VendorId: vendorId,
            Signature: sign,
            User: user,
            TransactionId: transactionId,
            Amount: amount);

        var fakeClient = Substitute.For<IGameApiClient>();

        var result = new ChangeBalanceResponse
        {
            ErrorCode = 0,
            Message = "操作成功",
            CurrentBalance = 200m,
            TransactionId = transactionId,
            Success = true,
            GameProviderTransactionId = "2147648076"
        };

        _ = fakeClient
            .ChangeBalanceAsync(Arg.Any<Uri>(), Arg.Any<ChangeBalanceRequest>())
            .Returns(result);

        var sut = new FundOutGetService(fakeClient);

        var actual = await sut
            .GetAsync(query)
            .ConfigureAwait(false);

        Assert.AreEqual(transactionId, actual.TransactionId);
        Assert.AreEqual("2147648076", actual.GameProviderTransactionId);
        Assert.AreEqual(200m, actual.CurrentBalance);
    }
}
