using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class FundOutGetService : IGetService<FundOutQuery, FundOutResult>
    {
        private readonly IGameApiClient _client;

        public FundOutGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<FundOutResult> GetAsync(FundOutQuery query, CancellationToken cancellationToken = default)
        {
            var request = new ChangeBalanceRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature,
                User = query.PlayerCode,
                Money = query.Amount,
                Order = query.TransactionId
            };
            var result = await _client.ChangeBalanceAsync(
                query.ApiUrl,
                request,
                cancellationToken)
                .ConfigureAwait(false);

            return new FundOutResult(
                TransactionId: result.Result!.YourOrderNum,
                GameProviderTransactionId: result.Result.OrderId,
                CurrentBalance: result.Result.Cash);
        }
    }
}
