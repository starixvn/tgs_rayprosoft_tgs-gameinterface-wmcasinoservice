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
                User = query.User,
                Money = query.Amount,
                Order = query.TransactionId
            };
            var result = await _client.ChangeBalanceAsync(
                query.ApiUrl,
                request,
                cancellationToken)
                .ConfigureAwait(false);

            return new FundOutResult(
                Success: result.Success,
                Message: result.Message,
                TransactionId: result.TransactionId,
                GameProviderTransactionId: result.GameProviderTransactionId,
                CurrentBalance: result.CurrentBalance);
        }
    }
}
