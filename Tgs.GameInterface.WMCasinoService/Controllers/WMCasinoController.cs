using Microsoft.AspNetCore.Mvc;
using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.ViewModels;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Controllers
{
    [Route("WMTF")]
    [ApiController]
    public class WMCasinoController : ControllerBase
    {
        /// <summary>
        /// Creates the player asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI",
        ///             "password": "TgsP@ssword",
        ///             "userName": "4ucmqplxb029"
        ///         }
        /// </remarks>
        [HttpPost("create")]
        public ValueTask CreatePlayerAsync(
                [FromServices] ICommandService<CreatePlayerCommand> service,
                CreatePlayerViewModel model)
                => service.ExecuteAsync(new CreatePlayerCommand(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.User,
                    model.Password,
                    model.UserName));

        /// <summary>
        /// Funds the in asynchronous.(GP -> Tgs)
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI",
        ///             "transactionId": "654BD1F6-BFD0-EA6D-AB72-471BF2758EB5",
        ///             "amount": 10
        ///         }
        /// </remarks>
        [HttpPost("fund-in")]
        public async ValueTask<FundResultViewModel> FundInAsync(
            [FromServices] IGetService<FundInQuery, FundInResult> service,
            FundQueryViewModel model)
        {
            var result = await service
                .GetAsync(new FundInQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.User,
                    model.TransactionId,
                    model.Amount))
                .ConfigureAwait(false);

            return new FundResultViewModel(
                result.Success,
                result.Message,
                result.GameProviderTransactionId,
                result.TransactionId,
                result.CurrentBalance);
        }

        /// <summary>
        /// Funds the out asynchronous.(Tgs -> Gp)
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI",
        ///             "transactionId": "654BD1F6-BFD0-EA6D-AB72-471BF2758EB5",
        ///             "amount": 100
        ///         }
        /// </remarks>
        [HttpPost("fund-out")]
        public async ValueTask<FundResultViewModel> FundOutAsync(
            [FromServices] IGetService<FundOutQuery, FundOutResult> service,
            FundQueryViewModel model)
        {
            var result = await service
                .GetAsync(new FundOutQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.User,
                    model.TransactionId,
                    model.Amount))
                .ConfigureAwait(false);

            return new FundResultViewModel(
                result.Success,
                result.Message,
                result.GameProviderTransactionId,
                result.TransactionId,
                result.CurrentBalance);
        }

        /// <summary>
        /// Get the Game URL asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI",
        ///             "password": "TgsP@ssword",
        ///             "language": 0
        ///         }
        /// </remarks>
        [HttpPost("gameurl")]
        public async ValueTask<GameUrlResultViewModel> GameUrlAsync(
            [FromServices] IGetService<GameUrlQuery, GameUrlResult> service,
            GameUrlQueryViewModel model)
        {
            var result = await service
                .GetAsync(new GameUrlQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.User,
                    model.Password,
                    model.Language))
                .ConfigureAwait(false);

            return new GameUrlResultViewModel(
                result.Success,
                result.IsPlayerExist,
                result.Message,
                result.GameUrl);
        }

        /// <summary>
        /// Gets the agent balance asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238"
        ///         }
        /// </remarks>
        [HttpPost("agentBalance")]
        public async ValueTask<AgentBalanceResultViewModel> GetAgentBalanceAsync(
            [FromServices] IGetService<AgentBalanceQuery, AgentBalanceResult> service,
            AgentBalanceQueryViewModel model)
        {
            var result = await service
                .GetAsync(new AgentBalanceQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature))
                .ConfigureAwait(false);

            return new AgentBalanceResultViewModel(
                result.Success,
                result.Message,
                result.Balance);
        }

        /// <summary>
        /// Get the Player balance asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI"
        ///         }
        /// </remarks>
        [HttpPost("balance")]
        public async ValueTask<BalanceResultViewModel> GetBalanceAsync(
            [FromServices] IGetService<BalanceQuery, BalanceResult> service,
            BalanceQueryViewModel model)
        {
            var result = await service
                .GetAsync(new BalanceQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.User))
                .ConfigureAwait(false);

            return new BalanceResultViewModel(
                result.Success,
                result.IsPlayerExist,
                result.Message,
                result.Balance);
        }

        /// <summary>
        /// Gets the game pull record asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "startTime": "2022-06-20T01:30:00.000Z",
        ///             "endTime": "2022-06-20T01:35:00.000Z"
        ///         }
        /// </remarks>
        [HttpPost("game/pullrecord")]
        public async IAsyncEnumerable<GamePullRecordResultViewModel> GetGamePullRecordAsync(
            [FromServices] IQueryService<GamePullRecordQuery, GamePullRecordResult> service,
            GamePullRecordQueryViewModel model)
        {
            var result = await service
                .QueryAsync(new GamePullRecordQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.StartTime,
                    model.EndTime))
                .ToArrayAsync()
                .ConfigureAwait(false);

            if (!result.Any())
                yield break;

            foreach (var item in result)
            {
                yield return new GamePullRecordResultViewModel
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
                    Subround = item.Subround,
                    TableId = item.TableId,
                    User = item.User,
                    Validbet = item.Validbet,
                    Water = item.Water,
                    Waterbet = item.Waterbet,
                    WinLoss = item.WinLoss
                };
            }
        }

        /// <summary>
        /// Gets the game un settle asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "time": "2022-06-16T02:11:54.603Z"
        ///         }
        /// </remarks>
        [HttpPost("game/unsettle")]
        public async IAsyncEnumerable<GameUnSettleTransactionViewModel> GetGameUnSettleAsync(
            [FromServices] IQueryService<GameUnSettleQuery, GameUnSettleResult> service,
            GameUnSettleQueryViewModel model)
        {
            var result = await service
                .QueryAsync(new GameUnSettleQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.Time))
                .ToArrayAsync()
                .ConfigureAwait(false);

            if (!result.Any())
                yield break;

            foreach (var item in result)
            {
                yield return new GameUnSettleTransactionViewModel
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

        /// <summary>
        /// Gets the member trade report.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI"
        ///         }
        /// </remarks>
        [HttpPost("transaction")]
        public async IAsyncEnumerable<MemberTradeViewModel> GetMemberTradeReport(
            [FromServices] IQueryService<MemberTradeQuery, MemberTradeResult> service,
            MemberTradeQueryViewModel model)
        {
            var result = await service
                .QueryAsync(new MemberTradeQuery(
                    model.ApiUrl,
                    model.VendorId,
                    model.Signature,
                    model.StartTime,
                    model.EndTime,
                    model.User,
                    model.TransactionId,
                    model.GameProviderTransactionId))
                .ToArrayAsync()
                .ConfigureAwait(false);

            if (!result.Any())
                yield break;

            foreach (var item in result)
            {
                yield return new MemberTradeViewModel
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

        /// <summary>
        /// Kicks the player asynchronous.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        /// <remarks>
        /// <b>Example</b><br />
        ///
        ///     Post
        ///         {
        ///             "apiUrl": "https://api.a45.me/api/public/Gateway.php",
        ///             "vendorId": "hpg02twapi",
        ///             "signature": "89448b444ed2e922ddc3aa0753370238",
        ///             "user": "wmtf_4ucmqplxb029_GPI"
        ///         }
        /// </remarks>
        [HttpPost("kick")]
        public ValueTask KickPlayerAsync(
            [FromServices] ICommandService<LogoutCommand> service,
            KickPlayerViewModel model)
        => service.ExecuteAsync(new LogoutCommand(
            model.ApiUrl,
            model.VendorId,
            model.Signature,
            model.User));
    }
}
