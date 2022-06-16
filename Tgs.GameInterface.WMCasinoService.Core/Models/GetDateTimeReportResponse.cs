using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetDateTimeReportResponse
    {
        /// <summary>
        /// 错误代码(0 :表示成功,其他为失败)
        /// </summary>
        /// <value>
        /// 0 该会员交易成功
        /// 107 操作成功，但未搜寻到数据
        /// 101 会员身分认证错误
        /// 102 时间格式错误
        /// </value>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; init; }
        /// <summary>
        /// 错误讯息
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; init; } = default!;
        [JsonPropertyName("result")]
        public GetDateTimeReportResult[] Result { get; init; } = default!;
    }
}
