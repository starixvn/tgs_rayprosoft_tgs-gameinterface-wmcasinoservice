using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record ChangeBalanceResult
    {
        /// <summary>
        /// PG的自订单号
        /// </summary>
        [JsonPropertyName("yourOrderNum")]
        public string? YourOrderNum { get; init; }
        /// <summary>
        /// WMCasino此次交易的单号<br />
        /// 例如:2147647425
        /// </summary>
        [JsonPropertyName("orderId")]
        public string OrderId { get; init; } = default!;
        /// <summary>
        /// 操作后金额
        /// </summary>
        [JsonPropertyName("cash")]
        public string Cash { get; init; } = default!;
    }
}
