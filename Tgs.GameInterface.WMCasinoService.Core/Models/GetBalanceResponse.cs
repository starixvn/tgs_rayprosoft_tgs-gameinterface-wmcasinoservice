using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetBalanceResponse
    {
        /// <summary>
        /// 错误代码(0 :表示成功,其他为失败)
        /// </summary>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; init; }
        /// <summary>
        /// 错误讯息
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; init; } = default!;
        /// <summary>
        /// 获取当前余额
        /// </summary>
        [JsonPropertyName("result")]
        public decimal? Result { get; init; }        
        public bool IsPlayerExist { get; init; }
    }
}
