namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record CreatePlayerCommand(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string User,
        string Password,
        string Username);
}
