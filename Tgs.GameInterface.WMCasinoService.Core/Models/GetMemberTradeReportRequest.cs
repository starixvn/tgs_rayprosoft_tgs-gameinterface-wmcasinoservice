using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    /// <summary>
    /// 使用此功能orderid 与 order 非必要代入，<br />
    /// 如只有带入user必须带入startTime与endTime,
    /// 否则只可查寻一小时内该用户的交易纪录，<br />
    /// 如果代入orderid与order则不受时间限制。
    /// </summary>
    public record GetMemberTradeReportRequest
    {
        /// <summary>
        /// 代理商(aid)
        /// </summary>
        [JsonPropertyName("vendorId")]
        public string VendorId { get; init; } = default!;
        /// <summary>
        /// 代理商标识符
        /// </summary>
        [JsonPropertyName("signature")]
        public string Signature { get; init; } = default!;
        /// <summary>
        /// 帐号
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; init; } = default!;
        /// <summary>
        /// WMCasino对该次交易纪录的编号
        /// </summary>
        [JsonPropertyName("orderid")]
        public string? OrderId { get; init; }
        /// <summary>
        /// PG对该次交易纪录自定的订单编号
        /// </summary>
        [JsonPropertyName("order")]
        public string? Order { get; init; }
        /// <summary>
        /// 例:20190314000000 (2019年03月14日00時00分00秒)
        /// </summary>
        [JsonPropertyName("startTime")]
        public string StartTime { get; init; } = default!;
        /// <summary>
        /// 例:20190314235959 (2019年03月14日23時59分59秒)
        /// </summary>
        [JsonPropertyName("endTime")]
        public string EndTime { get; init; } = default!;
    }
}
