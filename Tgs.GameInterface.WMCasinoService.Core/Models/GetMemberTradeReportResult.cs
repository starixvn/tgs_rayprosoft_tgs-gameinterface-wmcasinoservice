using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetMemberTradeReportResult
    {
        /// <summary>
        /// 该次交易单号
        /// </summary>
        /// <value>
        /// 2147647425
        /// </value>
        [JsonPropertyName("orderid")]
        public string OrderId { get; init; } = default!;
        /// <summary>
        /// 该次交易纪录时间
        /// </summary>
        /// <value>
        /// 2022-06-13 09:53:57
        /// </value>
        [JsonPropertyName("addtime")]
        public string AddTime { get; init; } = default!;
        /// <summary>
        ///
        /// </summary>
        /// <value>
        /// 803799
        /// </value>
        [JsonPropertyName("mid")]
        public string Mid { get; init; } = default!;
        /// <summary>
        /// 该次交易金额
        /// </summary>
        /// <value>
        /// 100.0000
        /// </value>
        [JsonPropertyName("money")]
        public string Money { get; init; } = default!;
        /// <summary>
        /// 加扣点代码:加点(121)扣点(122)
        /// </summary>
        /// <value>
        /// 121
        /// </value>
        [JsonPropertyName("op_code")]
        public string OpCode { get; init; } = default!;
        /// <summary>
        /// 此用户目前余额
        /// </summary>
        /// <value>
        /// 100.0000
        /// </value>
        [JsonPropertyName("subtotal")]
        public string SubTotal { get; init; } = default!;
        /// <summary>
        /// 该次交易 PG自订单号
        /// </summary>
        [JsonPropertyName("ordernum")]
        public string OrderNum { get; init; } = default!;
        /// <summary>
        /// 该次交易的帐号
        /// </summary>
        /// <value>
        /// playerCode
        /// </value>
        [JsonPropertyName("user")]
        public string User { get; init; } = default!;
    }
}
