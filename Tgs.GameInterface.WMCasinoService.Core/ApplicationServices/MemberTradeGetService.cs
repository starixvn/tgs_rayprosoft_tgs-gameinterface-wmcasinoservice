using System.Runtime.CompilerServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class MemberTradeGetService : IQueryService<MemberTradeQuery, MemberTradeResult>
{
    private readonly IGameApiClient _client;

    public MemberTradeGetService(IGameApiClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async IAsyncEnumerable<MemberTradeResult> QueryAsync(MemberTradeQuery query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        var request = new GetMemberTradeReportRequest
        {
            VendorId = query.VendorId,
            Signature = query.Signature,
            StartTime = query.StartTime,
            EndTime = query.EndTime,
            User = query.User,
            OrderId = query.GameProviderTransactionId,
            Order = query.TransactionId
        };

        var result = await _client
            .GetMemberTradeReportAsync(query.ApiUrl, request, cancellationToken)
            .ConfigureAwait(false);

        if (result is null || result.Result.Any() is false)
            yield break;

        foreach (var item in result.Result)
        {
            yield return new MemberTradeResult
            {
                AddTime = item.AddTime,
                Mid = item.Mid,
                Money = item.Money,
                OpCode = item.OpCode,
                OrderId = item.OrderId,
                OrderNum = item.OrderNum,
                SubTotal = item.SubTotal,
                User = item.User
            };
        }
    }
}
