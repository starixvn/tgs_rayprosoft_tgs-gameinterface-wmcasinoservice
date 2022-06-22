namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record GameUnSettleQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTimeOffset Time);
}
