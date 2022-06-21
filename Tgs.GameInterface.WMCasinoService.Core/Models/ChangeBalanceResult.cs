namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record ChangeBalanceResult
    {
        /// <summary>
        /// PG的自订单号
        /// </summary>
        public string? YourOrderNum { get; init; }
        /// <summary>
        /// WMCasino此次交易的单号<br />
        /// 例如:2147647425
        /// </summary>
        public string OrderId { get; init; } = default!;
        /// <summary>
        /// 操作后金额
        /// </summary>
        public decimal Cash { get; init; }
    }
}
