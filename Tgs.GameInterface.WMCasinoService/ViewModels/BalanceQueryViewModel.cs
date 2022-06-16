namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record BalanceQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode);
}
