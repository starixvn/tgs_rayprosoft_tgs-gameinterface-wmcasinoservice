using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record MemberRegisterRequest
    {
        /// <summary>
        /// 代理商(aid)
        /// </summary>        
        public string VendorId { get; init; } = default!;
        /// <summary>
        /// 代理商标识符
        /// </summary>        
        public string Signature { get; init; } = default!;
        /// <summary>
        /// 账号<br />
        /// 最小值:5字符<br />
        /// 最大值:30字符
        /// </summary>
        [JsonPropertyName("user")]
        public string User { get; init; } = default!;
        /// <summary>
        /// 密码
        /// 最小值:6字符   最大值:64字符
        /// </summary>
        [JsonPropertyName("password")]
        public string Password { get; init; } = default!;
        /// <summary>
        /// 姓名
        /// 最大值:30字符
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; init; } = default!;       
    }
}
