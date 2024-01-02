using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class AgentBalanceGetService(
    IGameApiClient client) : IGetService<AgentBalanceQuery, AgentBalanceResult>
{
    public async ValueTask<AgentBalanceResult> GetAsync(AgentBalanceQuery query, CancellationToken cancellationToken = default)
    {
        var request = new GetAgentBalanceRequest
        {
            VendorId = query.VendorId,
            Signature = query.Signature
        };
        var result = await client.GetAgentBalanceAsync(
            query.ApiUrl,
            request,
            cancellationToken).ConfigureAwait(false);

        return new AgentBalanceResult(
            result.Success,
            result.Message,
            result.Balance);
    }
}
