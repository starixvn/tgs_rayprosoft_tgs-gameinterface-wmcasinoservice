using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record LogoutGameRequest
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
        /// 帐号 (非必要,不填則登出所有會員)
        /// </summary>
        [JsonPropertyName("user")]
        public string? User { get; init; }
    }
}
