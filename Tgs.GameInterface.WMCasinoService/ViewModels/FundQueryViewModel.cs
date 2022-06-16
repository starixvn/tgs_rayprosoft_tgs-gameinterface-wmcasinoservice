namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record FundQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode,
        string TransactionId,
        decimal Amount);
}
