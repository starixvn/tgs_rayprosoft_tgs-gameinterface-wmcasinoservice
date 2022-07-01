using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class BalanceGetService : IGetService<BalanceQuery, BalanceResult>
    {
        private readonly IGameApiClient _client;

        public BalanceGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<BalanceResult> GetAsync(BalanceQuery query, CancellationToken cancellationToken = default)
        {
            var request = new GetBalanceRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature,
                User = query.User
            };
            var result = await _client.GetBalanceAsync(
                query.ApiUrl,
                request,
                cancellationToken).ConfigureAwait(false);

            return new BalanceResult(
                result.Success,
                result.IsPlayerExist,
                result.Message,
                result.Balance);
        }
    }
}
