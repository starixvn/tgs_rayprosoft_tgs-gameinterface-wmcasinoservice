namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record CreatePlayerCommand(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode,
        string Password,
        string Username);
}
