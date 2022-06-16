namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record ChangeBalanceRequest
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
        /// <summary>
        /// 加(扣)该会员的点数
        /// </summary>
        public decimal Money { get; init; }
        /// <summary>
        /// TGS产生的订单序号
        /// </summary>
        public string? Order { get; init; }
    }
}
