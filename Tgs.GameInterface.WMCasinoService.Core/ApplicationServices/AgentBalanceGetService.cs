using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class AgentBalanceGetService : IGetService<AgentBalanceQuery, AgentBalanceResult>
    {
        private readonly IGameApiClient _client;

        public AgentBalanceGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<AgentBalanceResult> GetAsync(AgentBalanceQuery query, CancellationToken cancellationToken = default)
        {
            var request = new GetAgentBalanceRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature
            };
            var result = await _client.GetAgentBalanceAsync(
                query.ApiUrl,
                request,
                cancellationToken).ConfigureAwait(false);

            return new AgentBalanceResult(
                result.Success,
                result.Message,
                result.Balance);
        }
    }
}
