namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record GamePullRecordQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime StartTime,
        DateTime? EndTime,
        string? PlayerCode);
}
