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
        var result = await memberTradeService.QueryAsync(
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
            .ConfigureAwait(false);

        var reply = new FundOrderReply();

        if (result is null)
        {
            return reply;
        }

        foreach (var transaction in result)
        {
            reply.Items.Add(new FundOrder()
            {
                AddTime = transaction.AddTime.ToTimestamp(),
                Mid = transaction.Mid,
                Money = transaction.Money,
                OpCode = transaction.OpCode,
                OrderId = transaction.OrderId,
                OrderNum = transaction.OrderNum,
                Subtotal = transaction.SubTotal,
                User = transaction.User
            });
        };

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
        var result = await gamePullService.QueryAsync(
            query: new GamePullRecordQuery(
                ApiUrl: new Uri(request.ApiUrl),
                VendorId: request.VendorId,
                Signature: request.Signature,
                StartTime: request.StartTime.ToDateTimeOffset(),
                EndTime: request.EndTime.ToDateTimeOffset()),
            cancellationToken: context.CancellationToken)
            .ToArrayAsync(
            cancellationToken: context.CancellationToken)
            .ConfigureAwait(false);

        var reply = new GamePullRecordReply();

        if (result is null)
        {
            return reply;
        }

        foreach (var record in result)
        {
            reply.Items.Add(new GamePullRecord()
            {
                User = record.User,
                BetId = record.BetId,
                BetTime = record.BetTime.ToTimestamp(),
                BeforeCash = record.BeforeCash,
                Bet = record.Bet,
                Validbet = record.Validbet,
                Water = record.Water,
                Result = record.Result,
                BetCode = record.BetCode,
                Waterbet = record.Waterbet,
                Winloss = record.WinLoss,
                Ip = record.Ip,
                Gid = record.Gid,
                Event = record.Event,
                EventChild = record.EventChild,
                Round = record.Round,
                Subround = record.Subround,
                TableId = record.TableId,
                Commission = record.Commission,
                Settime = record.Settime.ToTimestamp(),
                Reset = record.Reset,
                BetResult = record.BetResult,
                GameResult = record.GameResult,
                Gname = record.Gname,
                AdditionalData = record.AdditionalData,
            });
        };

        return reply;
    }

    public override async Task<GameUnsettleRecordReply> GameUnsettle(GameUnsettleRequest request, ServerCallContext context)
    {
        var result = await gameUnsettleService.QueryAsync(
            query: new GameUnSettleQuery(
                ApiUrl: new Uri(request.ApiUrl),
                VendorId: request.VendorId,
                Signature: request.Signature,
                Time: request.Time.ToDateTimeOffset()),
            cancellationToken: context.CancellationToken)
            .ToArrayAsync(
            cancellationToken: context.CancellationToken)
            .ConfigureAwait(false);

        var reply = new GameUnsettleRecordReply();

        if (result is null)
        {
            return reply;
        }

        foreach (var transaction in result)
        {
            reply.Items.Add(new GameUnsettleRecord()
            {
                User = transaction.User,
                BetId = transaction.BetId,
                Gid = transaction.Gid,
                Event = transaction.Event,
                EventChild = transaction.EventChild,
                BetTime = transaction.BetTime.ToTimestamp(),
                BetResult = transaction.BetResult,
                Bet = transaction.Bet,
                TableId = transaction.TableId,
                Commission = transaction.Commission,
                Round = transaction.Round,
                Subround = transaction.Subround,
                Gname = transaction.Gname,
                AdditionalData = transaction.AdditionalData,
            });
        };

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
