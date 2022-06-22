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
        public string VendorId { get; init; } = default!;
        /// <summary>
        /// 代理商标识符
        /// </summary>
        public string Signature { get; init; } = default!;
        /// <summary>
        /// 帐号
        /// </summary>
        public string? User { get; init; }
        /// <summary>
        /// WMCasino对该次交易纪录的编号
        /// </summary>
        public string? OrderId { get; init; }
        /// <summary>
        /// PG对该次交易纪录自定的订单编号
        /// </summary>
        public string? Order { get; init; }
        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTimeOffset? StartTime { get; init; }
        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTimeOffset? EndTime { get; init; }
    }
}
