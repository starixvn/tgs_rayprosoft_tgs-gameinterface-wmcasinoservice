using System.Runtime.CompilerServices;
using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class GamePullRecordGetService(
	IGameApiClient client) : IQueryService<GamePullRecordQuery, GamePullRecordResult>
{
	public async IAsyncEnumerable<GamePullRecordResult> QueryAsync(GamePullRecordQuery query, [EnumeratorCancellation] CancellationToken cancellationToken = default)
	{
		var request = new GetDateTimeReportRequest
		{
			VendorId = query.VendorId,
			Signature = query.Signature,
			StartTime = query.StartTime,
			EndTime = query.EndTime
		};

		var result = await client
			.GetDateTimeReportAsync(query.ApiUrl, request, cancellationToken)
			.ConfigureAwait(false);

		if (result is null || result.Result.Length == 0)
			yield break;

		foreach (var item in result.Result)
		{
			yield return new GamePullRecordResult
			{
				BeforeCash = item.BeforeCash,
				Bet = item.Bet,
				BetCode = item.BetCode,
				BetId = item.BetId,
				BetResult = item.BetResult,
				BetTime = item.BetTime,
				Commission = item.Commission,
				Event = item.Event,
				EventChild = item.EventChild,
				GameResult = item.GameResult,
				Gid = item.Gid,
				Gname = item.Gname,
				Ip = item.Ip,
				Reset = item.Reset,
				Result = item.Result,
				Round = item.Round,
				Settime = item.Settime,
				Subround = item.SubRound,
				TableId = item.TableId,
				User = item.User,
				Validbet = item.Validbet,
				Water = item.Water,
				Waterbet = item.Waterbet,
				WinLoss = item.WinLoss,
				AdditionalData = item.AdditionalData,
			};
		}
	}
}
