using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Protos;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.GrpcServices;

public class GameInterfaceWMCasinoGrpcService(
	ILogger<GameInterfaceWMCasinoGrpcService> logger,
	IGetService<FundInQuery, FundInResult> fundInService,
	IQueryService<MemberTradeQuery, MemberTradeResult> memberTradeService,
	IGetService<FundOutQuery, FundOutResult> fundOutService,
	IGetService<BalanceQuery, BalanceResult> balanceService,
	IGetService<AgentBalanceQuery, AgentBalanceResult> agentBalanceService,
	ICommandService<LogoutCommand> logoutService,
	ICommandService<CreatePlayerCommand> createPlayerService,
	IGetService<GameUrlQuery, GameUrlResult> gameUrlService,
	IQueryService<GamePullRecordQuery, GamePullRecordResult> gamePullService,
	IQueryService<GameUnSettleQuery, GameUnSettleResult> gameUnsettleService) : GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceBase
{
	public override Task<CheckingServiceReply> CheckingService(CheckingServiceRequest request, ServerCallContext context)
	{
		logger.LogWarning("LogOn:{logOn} Message: Create a check connection.({machineName})", DateTime.Now.ToString("o"), request.MachineName);

		return Task.FromResult(new CheckingServiceReply
		{
			MachineName = Environment.MachineName
		});
	}

	public override async Task<Reply> CreatePlayer(CreatePlayerRequest request, ServerCallContext context)
	{
		await createPlayerService.ExecuteAsync(
			command: new CreatePlayerCommand(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User,
				Password: request.Password,
				Username: request.UserName),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new Reply()
		{
			Success = true
		};
	}

	public override async Task<FundReply> FundIn(FundRequest request, ServerCallContext context)
	{
		var result = await fundInService.GetAsync(
			query: new FundInQuery(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User,
				TransactionId: request.TransactionId,
				Amount: request.Amount),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new FundReply()
		{
			Success = result.Success,
			Message = result.Message ?? string.Empty,
			TransactionId = result.TransactionId ?? string.Empty,
			GameProviderTransactionId = result.GameProviderTransactionId ?? string.Empty,
			CurrentBalance = result.CurrentBalance ?? decimal.MinusOne,
		};
	}

	public override async Task<FundOrderReply> FundOrder(FundOrderRequest request, ServerCallContext context)
	{
		var memberTrades = await memberTradeService.QueryAsync(
			query: new MemberTradeQuery(
				ApiUrl: new Uri(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				StartTime: request.StartTime.ToDateTimeOffset(),
				EndTime: request.EndTime.ToDateTimeOffset(),
				User: request.User,
				TransactionId: request.TransactionId,
				GameProviderTransactionId: request.GameProviderTransactionId),
			cancellationToken: context.CancellationToken)
			.ToArrayAsync(
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false)
			?? Enumerable.Empty<MemberTradeResult>();

		var reply = new FundOrderReply();

		if (memberTrades.Any())
		{
			reply.Items.AddRange(memberTrades.Select(trade => new FundOrder
			{
				AddTime = trade.AddTime.ToTimestamp(),
				Mid = trade.Mid,
				Money = trade.Money,
				OpCode = trade.OpCode,
				OrderId = trade.OrderId,
				OrderNum = trade.OrderNum,
				Subtotal = trade.SubTotal,
				User = trade.User
			}));
		}

		return reply;
	}

	public override async Task<FundReply> FundOut(FundRequest request, ServerCallContext context)
	{
		var result = await fundOutService.GetAsync(
			query: new FundOutQuery(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User,
				TransactionId: request.TransactionId,
				Amount: request.Amount),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new FundReply()
		{
			Success = result.Success,
			Message = result.Message ?? string.Empty,
			TransactionId = result.TransactionId ?? string.Empty,
			GameProviderTransactionId = result.GameProviderTransactionId ?? string.Empty,
			CurrentBalance = result.CurrentBalance ?? decimal.MinusOne,
		};
	}

	public override async Task<GamePullRecordReply> GamePullRecord(GamePullRecordeRequest request, ServerCallContext context)
	{
		var gamePullRecords = await gamePullService.QueryAsync(
			query: new GamePullRecordQuery(
				ApiUrl: new Uri(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				StartTime: request.StartTime.ToDateTimeOffset(),
				EndTime: request.EndTime.ToDateTimeOffset()),
			cancellationToken: context.CancellationToken)
			.ToArrayAsync(
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false)
			?? Enumerable.Empty<GamePullRecordResult>();

		var reply = new GamePullRecordReply();

		if (gamePullRecords.Any())
		{
			reply.Items.AddRange(gamePullRecords.Select(gamePullRecord => new GamePullRecord
			{
				User = gamePullRecord.User,
				BetId = gamePullRecord.BetId,
				BetTime = gamePullRecord.BetTime.ToTimestamp(),
				BeforeCash = gamePullRecord.BeforeCash,
				Bet = gamePullRecord.Bet,
				Validbet = gamePullRecord.Validbet,
				Water = gamePullRecord.Water,
				Result = gamePullRecord.Result,
				BetCode = gamePullRecord.BetCode,
				Waterbet = gamePullRecord.Waterbet,
				Winloss = gamePullRecord.WinLoss,
				Ip = gamePullRecord.Ip,
				Gid = gamePullRecord.Gid,
				Event = gamePullRecord.Event,
				EventChild = gamePullRecord.EventChild,
				Round = gamePullRecord.Round,
				Subround = gamePullRecord.Subround,
				TableId = gamePullRecord.TableId,
				Commission = gamePullRecord.Commission,
				Settime = gamePullRecord.Settime.ToTimestamp(),
				Reset = gamePullRecord.Reset,
				BetResult = gamePullRecord.BetResult,
				GameResult = gamePullRecord.GameResult,
				Gname = gamePullRecord.Gname,
				AdditionalData = gamePullRecord.AdditionalData,
			}));
		}

		return reply;
	}

	public override async Task<GameUnsettleRecordReply> GameUnsettle(GameUnsettleRequest request, ServerCallContext context)
	{
		var gameUnSettles = await gameUnsettleService.QueryAsync(
			query: new GameUnSettleQuery(
				ApiUrl: new Uri(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				Time: request.Time.ToDateTimeOffset()),
			cancellationToken: context.CancellationToken)
			.ToArrayAsync(
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false)
			?? Enumerable.Empty<GameUnSettleResult>();

		var reply = new GameUnsettleRecordReply();

		if (gameUnSettles.Any())
		{
			reply.Items.AddRange(gameUnSettles.Select(gameUnSettle => new GameUnsettleRecord()
			{
				User = gameUnSettle.User,
				BetId = gameUnSettle.BetId,
				Gid = gameUnSettle.Gid,
				Event = gameUnSettle.Event,
				EventChild = gameUnSettle.EventChild,
				BetTime = gameUnSettle.BetTime.ToTimestamp(),
				BetResult = gameUnSettle.BetResult,
				Bet = gameUnSettle.Bet,
				TableId = gameUnSettle.TableId,
				Commission = gameUnSettle.Commission,
				Round = gameUnSettle.Round,
				Subround = gameUnSettle.Subround,
				Gname = gameUnSettle.Gname,
				AdditionalData = gameUnSettle.AdditionalData,
			}));
		}

		return reply;
	}

	public override async Task<GameUrlReply> GameUrl(GameUrlRequest request, ServerCallContext context)
	{
		var result = await gameUrlService.GetAsync(
			query: new GameUrlQuery(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User,
				Password: request.Password,
				Language: request.Language),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new GameUrlReply()
		{
			Success = result.Success,
			Message = result.Message ?? string.Empty,
			IsPlayerExist = result.IsPlayerExist,
			GameUrl = result.GameUrl ?? string.Empty,
		};
	}

	public override async Task<AgentBalanceReply> GetAgentBalance(AgentBalanceRequest request, ServerCallContext context)
	{
		var result = await agentBalanceService.GetAsync(
			query: new AgentBalanceQuery(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new AgentBalanceReply()
		{
			Success = result.Success,
			Message = result.Message ?? string.Empty,
			Balance = result.Balance ?? decimal.Zero,
		};
	}

	public override async Task<BalanceReply> GetBalance(BalanceRequest request, ServerCallContext context)
	{
		var result = await balanceService.GetAsync(
			query: new BalanceQuery(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new BalanceReply()
		{
			Success = result.Success,
			Message = result.Message ?? string.Empty,
			IsPlayerExist = result.IsPlayerExist,
			Balance = result.Balance ?? decimal.Zero,
		};
	}

	public override async Task<Reply> KickPlayer(KickPlayerRequest request, ServerCallContext context)
	{
		await logoutService.ExecuteAsync(
			command: new LogoutCommand(
				ApiUrl: new(request.ApiUrl),
				VendorId: request.VendorId,
				Signature: request.Signature,
				User: request.User),
			cancellationToken: context.CancellationToken)
			.ConfigureAwait(false);

		return new Reply()
		{
			Success = true
		};
	}
}
