using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Protos;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.GrpcServices
{
    public class GameInterfaceWMCasinoGrpcService : GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceBase
    {
        private readonly IGetService<BalanceQuery, BalanceResult> _balanceService;
        private readonly ICommandService<CreatePlayerCommand> _createPlayerService;
        private readonly IGetService<FundInQuery, FundInResult> _fundInService;
        private readonly IGetService<FundOutQuery, FundOutResult> _fundOutService;
        private readonly IQueryService<GamePullRecordQuery, GamePullRecordResult> _gamePullService;
        private readonly IQueryService<GameUnSettleQuery, GameUnSettleResult> _gameUnsettleService;
        private readonly IGetService<GameUrlQuery, GameUrlResult> _gameUrlService;
        private readonly ICommandService<LogoutCommand> _logoutService;
        private readonly IQueryService<MemberTradeQuery, MemberTradeResult> _memberTradeService;

        public GameInterfaceWMCasinoGrpcService(
            IGetService<FundInQuery, FundInResult> fundInService,
            IQueryService<MemberTradeQuery, MemberTradeResult> memberTradeService,
            IGetService<FundOutQuery, FundOutResult> fundOutService,
            IGetService<BalanceQuery, BalanceResult> balanceService,
            ICommandService<LogoutCommand> logoutService,
            ICommandService<CreatePlayerCommand> createPlayerService,
            IGetService<GameUrlQuery, GameUrlResult> gameUrlService,
            IQueryService<GamePullRecordQuery, GamePullRecordResult> gamePullService,
            IQueryService<GameUnSettleQuery, GameUnSettleResult> gameUnsettleService)
        {
            _fundInService = fundInService ?? throw new ArgumentNullException(nameof(fundInService));
            _memberTradeService = memberTradeService ?? throw new ArgumentNullException(nameof(memberTradeService));
            _fundOutService = fundOutService ?? throw new ArgumentNullException(nameof(fundOutService));
            _balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
            _logoutService = logoutService ?? throw new ArgumentNullException(nameof(logoutService));
            _createPlayerService = createPlayerService ?? throw new ArgumentNullException(nameof(createPlayerService));
            _gameUrlService = gameUrlService ?? throw new ArgumentNullException(nameof(gameUrlService));
            _gamePullService = gamePullService ?? throw new ArgumentNullException(nameof(gamePullService));
            _gameUnsettleService = gameUnsettleService ?? throw new ArgumentNullException(nameof(gameUnsettleService));
        }

        public override async Task<GameInterfaceWMCasinoServiceReply> CreatePlayer(GameInterfaceWMCasinoServiceCreatePlayerRequest request, ServerCallContext context)
        {
            await _createPlayerService.ExecuteAsync(new CreatePlayerCommand(
                       new(request.ApiUrl),
                       request.VendorId,
                       request.Signature,
                       request.User,
                       request.Password,
                       request.UserName)).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceReply()
            {
                Success = true
            };
        }

        public override async Task<GameInterfaceWMCasinoServiceFundReply> FundIn(GameInterfaceWMCasinoServiceFundRequest request, ServerCallContext context)
        {
            var result = await _fundInService.GetAsync(new FundInQuery(
                new(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.User,
                request.TransactionId,
                request.Amount)).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceFundReply()
            {
                Success = result.Success,
                Message = result.Message ?? string.Empty,
                TransactionId = result.TransactionId ?? string.Empty,
                GameProviderTransactionId = result.GameProviderTransactionId ?? string.Empty,
                CurrentBalance = result.CurrentBalance ?? decimal.MinusOne,
            };
        }

        public override async Task<GameInterfaceWMCasinoServiceFundOrderReply> FundOrder(GameInterfaceWMCasinoServiceFundOrderRequest request, ServerCallContext context)
        {
            var result = await _memberTradeService.QueryAsync(new MemberTradeQuery(
                new Uri(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.StartTime.ToDateTimeOffset(),
                request.EndTime.ToDateTimeOffset(),
                request.User,
                request.TransactionId,
                request.GameProviderTransactionId
            )).ToArrayAsync().ConfigureAwait(false);

            var reply = new GameInterfaceWMCasinoServiceFundOrderReply();

            if (result is null)
            {
                return reply;
            }

            foreach (var transaction in result)
            {
                reply.Items.Add(new GameInterfaceWMCasinoServiceFundOrder()
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

        public override async Task<GameInterfaceWMCasinoServiceFundReply> FundOut(GameInterfaceWMCasinoServiceFundRequest request, ServerCallContext context)
        {
            var result = await _fundOutService.GetAsync(new FundOutQuery(
                new(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.User,
                request.TransactionId,
                request.Amount)).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceFundReply()
            {
                Success = result.Success,
                Message = result.Message ?? string.Empty,
                TransactionId = result.TransactionId ?? string.Empty,
                GameProviderTransactionId = result.GameProviderTransactionId ?? string.Empty,
                CurrentBalance = result.CurrentBalance ?? decimal.MinusOne,
            };
        }

        public override async Task<GameInterfaceWMCasinoServiceGamePullRecordReply> GamePullRecord(GameInterfaceWMCasinoServiceGamePullRecordeRequest request, ServerCallContext context)
        {
            var result = await _gamePullService.QueryAsync(new GamePullRecordQuery(
                new Uri(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.StartTime.ToDateTimeOffset(),
                request.EndTime.ToDateTimeOffset()
            )).ToArrayAsync().ConfigureAwait(false);

            var reply = new GameInterfaceWMCasinoServiceGamePullRecordReply();

            if (result is null)
            {
                return reply;
            }

            foreach (var record in result)
            {
                reply.Items.Add(new GameInterfaceWMCasinoServiceGamePullRecord()
                {
                    BeforeCash = record.BeforeCash,
                    Bet = record.Bet,
                    BetCode = record.BetCode,
                    BetId = record.BetId,
                    BetResult = record.BetResult,
                    BetTime = record.BetTime.ToTimestamp(),
                    Commission = record.Commission,
                    Event = record.Event,
                    EventChild = record.EventChild,
                    GameResult = record.GameResult,
                    Gid = record.Gid,
                    Gname = record.Gname,
                    Ip = record.Ip,
                    Reset = record.Reset,
                    Result = record.Result,
                    Round = record.Round,
                    Settime = record.Settime.ToTimestamp(),
                    Subround = record.Subround,
                    TableId = record.TableId,
                    User = record.User,
                    Validbet = record.Validbet,
                    Water = record.Water,
                    Waterbet = record.Waterbet,
                    Winloss = record.WinLoss
                });
            };

            return reply;
        }

        public override async Task<GameInterfaceWMCasinoServiceGameUnsettleRecordReply> GameUnsettle(GameInterfaceWMCasinoServiceGameUnsettleRequest request, ServerCallContext context)
        {
            var result = await _gameUnsettleService.QueryAsync(new GameUnSettleQuery(
                new Uri(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.Time.ToDateTimeOffset()
            )).ToArrayAsync().ConfigureAwait(false);

            var reply = new GameInterfaceWMCasinoServiceGameUnsettleRecordReply();

            if (result is null)
            {
                return reply;
            }

            foreach (var transaction in result)
            {
                reply.Items.Add(new GameInterfaceWMCasinoServiceGameUnsettleRecord()
                {
                    TableId = transaction.TableId,
                    Bet = transaction.Bet,
                    BetId = transaction.BetId,
                    BetResult = transaction.BetResult,
                    BetTime = transaction.BetTime.ToTimestamp(),
                    Commission = transaction.Commission,
                });
            };

            return reply;
        }

        public override async Task<GameInterfaceWMCasinoServiceGameUrlReply> GameUrl(GameInterfaceWMCasinoServiceGameUrlRequest request, ServerCallContext context)
        {
            var result = await _gameUrlService.GetAsync(new GameUrlQuery(
                ApiUrl: new(request.ApiUrl),
                VendorId: request.VendorId,
                Signature: request.Signature,
                User: request.User,
                Password: request.Password,
                Language: request.Language
                )).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceGameUrlReply()
            {
                Success = result.Success,
                Message = result.Message ?? string.Empty,
                IsPlayerExist = result.IsPlayerExist,
                GameUrl = result.GameUrl ?? string.Empty,
            };
        }

        public override async Task<GameInterfaceWMCasinoServiceBalanceReply> GetBalance(GameInterfaceWMCasinoServiceBalanceRequest request, ServerCallContext context)
        {
            var result = await _balanceService.GetAsync(new BalanceQuery(
                new(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.User)).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceBalanceReply()
            {
                Success = result.Success,
                Message = result.Message ?? string.Empty,
                IsPlayerExist = result.IsPlayerExist,
                Balance = result.Balance ?? decimal.Zero,
            };
        }

        public override async Task<GameInterfaceWMCasinoServiceReply> KickPlayer(GameInterfaceWMCasinoServiceKickPlayerRequest request, ServerCallContext context)
        {
            await _logoutService.ExecuteAsync(new LogoutCommand(
                new(request.ApiUrl),
                request.VendorId,
                request.Signature,
                request.User)).ConfigureAwait(false);

            return new GameInterfaceWMCasinoServiceReply()
            {
                Success = true
            };
        }
    }
}
