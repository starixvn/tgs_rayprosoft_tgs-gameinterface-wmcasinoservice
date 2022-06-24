namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record MemberTradeViewModel
    {
        /// <summary>
        /// 该次交易单号
        /// </summary>
        /// <value>
        /// 2147647425
        /// </value>
        public string OrderId { get; init; } = default!;
        /// <summary>
        /// 该次交易纪录时间
        /// </summary>
        /// <value>
        /// 2022-06-13 09:53:57
        /// </value>
        public DateTimeOffset AddTime { get; init; }
        /// <summary>
        ///
        /// </summary>
        /// <value>
        /// 803799
        /// </value>
        public string Mid { get; init; } = default!;
        /// <summary>
        /// 该次交易金额
        /// </summary>
        /// <value>
        /// 100.0000
        /// </value>
        public decimal Money { get; init; }
        /// <summary>
        /// 加扣点代码:加点(121)扣点(122)
        /// </summary>
        /// <value>
        /// 121
        /// </value>
        public string OpCode { get; init; } = default!;
        /// <summary>
        /// 此用户目前余额
        /// </summary>
        /// <value>
        /// 100.0000
        /// </value>
        public decimal SubTotal { get; init; }
        /// <summary>
        /// 该次交易 PG自订单号
        /// </summary>
        public string OrderNum { get; init; } = default!;
        /// <summary>
        /// 该次交易的帐号
        /// </summary>
        /// <value>
        /// wmtf_{playerCode}_{brandCode}
        /// </value>
        public string User { get; init; } = default!;
    }
}
