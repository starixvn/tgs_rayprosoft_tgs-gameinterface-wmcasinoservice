namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record FundOutQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode,
        string TransactionId,
        decimal Amount);
}
