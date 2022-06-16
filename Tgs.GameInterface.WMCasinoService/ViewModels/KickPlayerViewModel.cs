namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record KickPlayerViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string? PlayerCode);
}
