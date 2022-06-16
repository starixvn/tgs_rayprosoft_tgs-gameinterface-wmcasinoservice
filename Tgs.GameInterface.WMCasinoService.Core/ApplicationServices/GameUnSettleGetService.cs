using System.Runtime.CompilerServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class GameUnSettleGetService : IQueryService<GameUnSettleQuery, GameUnSettleResult>
    {
        private readonly IGameApiClient _client;

        public GameUnSettleGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async IAsyncEnumerable<GameUnSettleResult> QueryAsync(GameUnSettleQuery query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var request = new GetUnsettleReportRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature,
                Time = query.Time
            };
            var result = await _client
                .GetUnsettleReportAsync(query.ApiUrl, request, cancellationToken)
                .ConfigureAwait(false);

            if (result is null || result.Result.Any() is false)
                yield break;

            foreach (var item in result.Result)
            {
                yield return new GameUnSettleResult
                {
                    Bet = item.Bet,
                    BetId = item.BetId,
                    BetResult = item.BetResult,
                    BetTime = item.BetTime,
                    Commission = item.Commission,
                    Event = item.Event,
                    EventChild = item.EventChild,
                    Gid = item.Gid,
                    Gname = item.Gname,
                    Round = item.Round,
                    Subround = item.Subround,
                    TableId = item.TableId,
                    User = item.User,
                };
            }
        }
    }
}
