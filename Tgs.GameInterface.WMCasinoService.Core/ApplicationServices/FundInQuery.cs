namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record FundInQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string PlayerCode,
        string TransactionId,
        decimal Amount);
}
