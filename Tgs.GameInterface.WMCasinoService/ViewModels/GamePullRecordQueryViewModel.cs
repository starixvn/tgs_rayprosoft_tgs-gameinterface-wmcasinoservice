namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record GamePullRecordQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime StartTime,
        DateTime? EndTime,
        string? PlayerCode);
}
