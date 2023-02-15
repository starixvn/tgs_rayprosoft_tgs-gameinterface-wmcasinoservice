namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record FundInQuery(
    Uri ApiUrl,
    string VendorId,
    string Signature,
    string User,
    string TransactionId,
    decimal Amount);
