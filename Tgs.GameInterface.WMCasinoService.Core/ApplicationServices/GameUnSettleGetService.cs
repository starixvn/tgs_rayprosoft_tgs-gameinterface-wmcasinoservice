using System.Runtime.CompilerServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class GameUnSettleGetService(
    IGameApiClient client) : IQueryService<GameUnSettleQuery, GameUnSettleResult>
{
    public async IAsyncEnumerable<GameUnSettleResult> QueryAsync(GameUnSettleQuery query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new GetUnsettleReportRequest
        {
            VendorId = query.VendorId,
            Signature = query.Signature,
            Time = query.Time
        };
        var result = await client
            .GetUnsettleReportAsync(query.ApiUrl, request, cancellationToken)
            .ConfigureAwait(false);

        if (result is null || result.Result.Length == 0)
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
                Subround = item.SubRound,
                TableId = item.TableId,
                User = item.User,
                AdditionalData = item.AdditionalData
            };
        }
    }
}
