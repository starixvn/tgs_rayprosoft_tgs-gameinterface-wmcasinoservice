namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record GameUnSettleQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime Time);
}
