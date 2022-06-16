using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class FundInGetService : IGetService<FundInQuery, FundInResult>
    {
        private readonly IGameApiClient _client;

        public FundInGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<FundInResult> GetAsync(FundInQuery query, CancellationToken cancellationToken = default)
        {
            var request = new ChangeBalanceRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature,
                User = query.PlayerCode,
                Money = - query.Amount,
                Order = query.TransactionId
            };

            var result = await _client.ChangeBalanceAsync(
                query.ApiUrl,
                request,
                cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return new FundInResult(
                GameProviderTransactionId: result.Result.OrderId,
                TransactionId: result.Result.YourOrderNum,
                CurrentBalance: decimal.Parse(result.Result.Cash));
        }
    }
}
