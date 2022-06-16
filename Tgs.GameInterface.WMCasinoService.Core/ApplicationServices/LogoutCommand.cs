namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record LogoutCommand(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string? PlayerCode);
}
