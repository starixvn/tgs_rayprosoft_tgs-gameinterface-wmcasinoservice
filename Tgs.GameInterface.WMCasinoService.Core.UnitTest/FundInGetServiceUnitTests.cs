using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest
{
    [TestClass]
    public class FundInGetServiceUnitTests
    {
        [TestMethod]
        public async Task Normal_Process()
        {
            var apiUrl = new Uri("http://localhost/");
            var vendorId = "testvendorid";
            var sign = "testsign";
            var playerCode = "4ucmqplxb029";
            string transactionId = Guid.NewGuid().ToString("N");
            decimal amount = 100m;

            var query = new FundInQuery(
                ApiUrl: apiUrl,
                VendorId: vendorId,
                Signature: sign,
                PlayerCode: playerCode,
                TransactionId: transactionId,
                Amount: amount);

            var fakeClient = Substitute.For<IGameApiClient>();

            var balanceResult = new ChangeBalanceResult
            {
                Cash = "200",
                OrderId = "2147648076",
                YourOrderNum = transactionId
            };

            var result = new ChangeBalanceResponse
            {
                ErrorCode = 0,
                ErrorMessage = "操作成功",
                Result = balanceResult
            };

            _ = fakeClient
                .ChangeBalanceAsync(Arg.Any<Uri>(), Arg.Any<ChangeBalanceRequest>())
                .Returns(result);

            var sut = new FundInGetService(fakeClient);

            var actual = await sut
                .GetAsync(query)
                .ConfigureAwait(false);

            Assert.AreEqual(transactionId, actual.TransactionId);
            Assert.AreEqual("2147648076", actual.GameProviderTransactionId);
            Assert.AreEqual(200m, actual.CurrentBalance);
        }
    }
}
