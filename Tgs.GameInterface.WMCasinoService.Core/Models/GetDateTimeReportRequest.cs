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
        public string VendorId { get; init; } = default!;
        /// <summary>
        /// 代理商标识符
        /// </summary>
        public string Signature { get; init; } = default!;
        /// <summary>
        /// 帐号
        /// </summary>
        /// <value>
        /// playerCode
        /// </value>
        public string? User { get; init; }
        /// <summary>
        /// Gets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public DateTime StartTime { get; init; } = default!;
        /// <summary>
        /// Gets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public DateTime? EndTime { get; init; }
        /// <summary>
        /// 0:抓下注时间, 1:抓结算时间
        /// </summary>
        public int? TimeType { get; init; }
        /// <summary>
        /// 0:输赢报表, 1:小费报表, 2:全部
        /// </summary>
        public int? DataType { get; init; }
        /// <summary>
        /// 期数
        /// </summary>
        public int? GameNo1 { get; init; }
    }
}
