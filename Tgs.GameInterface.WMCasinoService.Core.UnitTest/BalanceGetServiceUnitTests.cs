using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core.UnitTest
{
    [TestClass]
    public class BalanceGetServiceUnitTests
    {
        [TestMethod]
        public async Task Normal_Process()
        {
            var fakeClient = Substitute.For<IGameApiClient>();

            var sut = new BalanceGetService(fakeClient);

            var apiUrl = new Uri("http://localhost/");
            var vedorId = "testVendorId";
            var sign = "testsign";
            var playerCode = "4ucmqplxb029";

            var query = new BalanceQuery(apiUrl, vedorId, sign, playerCode);

            var request = new GetBalanceRequest
            {
                VendorId = vedorId,
                Signature = sign,
                User = playerCode
            };

            var result = new GetBalanceResponse
            {
                ErrorCode = 0,
                ErrorMessage = "操作成功",
                IsPlayerExist = true,
                Result = 168m
            };

            _ = fakeClient.GetBalanceAsync(Arg.Is(apiUrl), Arg.Is(request))
                .Returns(result);

            var actual = await sut.GetAsync(query).ConfigureAwait(false);

            Assert.IsTrue(actual.IsPlayerExist);
            Assert.AreEqual(168m, actual.Balance);
        }

        [TestMethod]
        public async Task Account_Is_Not_Exist()
        {
            var fakeClient = Substitute.For<IGameApiClient>();

            var sut = new BalanceGetService(fakeClient);

            var apiUrl = new Uri("http://localhost/");
            var vedorId = "testVendorId";
            var sign = "testsign";
            var playerCode = "4ucmqplxb028";

            var query = new BalanceQuery(apiUrl, vedorId, sign, playerCode);

            var request = new GetBalanceRequest
            {
                VendorId = vedorId,
                Signature = sign,
                User = playerCode
            };

            var result = new GetBalanceResponse
            {
                ErrorCode = 10501,
                ErrorMessage = "查无此帐号,请检查",
                IsPlayerExist = false,
                Result = null
            };

            _ = fakeClient.GetBalanceAsync(Arg.Is(apiUrl), Arg.Is(request))
                .Returns(result);

            var actual = await sut.GetAsync(query).ConfigureAwait(false);

            Assert.IsFalse(actual.IsPlayerExist);
            Assert.IsNull(actual.Balance);
        }
    }
}
