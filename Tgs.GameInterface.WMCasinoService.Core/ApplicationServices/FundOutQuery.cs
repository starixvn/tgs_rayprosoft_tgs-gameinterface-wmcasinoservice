namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record FundOutQuery(
    Uri ApiUrl,
    string VendorId,
    string Signature,
    string User,
    string TransactionId,
    decimal Amount);
