namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record BalanceQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        string User);
}
