namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetBalanceRequest
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
        /// 帐号
        /// </summary>
        public string User { get; init; } = default!;
    }
}
