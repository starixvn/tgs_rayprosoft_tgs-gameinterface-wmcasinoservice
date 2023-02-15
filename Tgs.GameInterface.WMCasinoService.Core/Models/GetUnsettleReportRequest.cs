namespace Tgs.GameInterface.WMCasinoService.Core.Models;

public record GetUnsettleReportRequest
{
    /// <summary>
    /// 代理商(aid)
    /// </summary>
    public string VendorId { get; init; } = default!;
    /// <summary>
    /// 代理商标识符
    /// </summary>
    public string Signature { get; init; } = default!;

    public DateTimeOffset Time { get; init; }
}
