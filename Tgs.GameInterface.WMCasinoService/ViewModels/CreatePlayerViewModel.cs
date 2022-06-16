namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record CreatePlayerViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode,
        string Password,
        string UserName);
}
