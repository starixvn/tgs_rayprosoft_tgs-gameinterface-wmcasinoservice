using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetUnsettleReportResponse
    {
        /// <summary>
        /// 错误代码(0 :表示成功,其他为失败)
        /// </summary>
        /// <value>
        /// 0 该会员操作成功<br/>
        /// 107 操作成功，但未搜寻到数据<br/>
        /// 102 时间格式错误<br/>
        /// 10411 請於30秒後再試
        /// </value>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; init; }
        /// <summary>
        /// 错误讯息
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; init; } = default!;

        [JsonPropertyName("result")]
        public GetUnsettleReportResult[] Result { get; init; } = default!;
    }
}
