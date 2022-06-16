using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    /// <summary>
    /// 如欲查询全部用户就不用输入user，会以时间区间进行呈现。<br />
    /// 如果报表是要搜寻到起始时间到现在，endTime即可以不用代值，只需带startTime。
    /// </summary>
    public record GetDateTimeReportRequest
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
        /// <value>
        /// playerCode
        /// </value>
        [JsonPropertyName("user")]
        public string? User { get; init; }
        /// <summary>
        /// 例:20170809130500 (2017年08月09日 13点05分00
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        [JsonPropertyName("startTime")]
        public string StartTime { get; init; } = default!;
        /// <summary>
        /// 例:20170809130600 (2017年08月09日 13点06分00秒)
        /// </summary>
        [JsonPropertyName("endTime")]
        public string? EndTime { get; init; }
        /// <summary>
        /// 0:抓下注时间, 1:抓结算时间
        /// </summary>
        [JsonPropertyName("timetype")]
        public int? TimeType { get; init; }
        /// <summary>
        /// 0:输赢报表, 1:小费报表, 2:全部
        /// </summary>
        [JsonPropertyName("datatype")]
        public int? DataType { get; init; }
        /// <summary>
        /// 期数
        /// </summary>
        [JsonPropertyName("gameno1")]
        public int? GameNo1 { get; init; }
    }
}
