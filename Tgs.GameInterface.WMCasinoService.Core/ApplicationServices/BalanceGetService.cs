using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class BalanceGetService(
    IGameApiClient client) : IGetService<BalanceQuery, BalanceResult>
{
    public async ValueTask<BalanceResult> GetAsync(BalanceQuery query, CancellationToken cancellationToken = default)
    {
        var request = new GetBalanceRequest
        {
            VendorId = query.VendorId,
            Signature = query.Signature,
            User = query.User
        };
        var result = await client.GetBalanceAsync(
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
