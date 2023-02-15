namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record FundQueryViewModel(
    Uri ApiUrl,
    string VendorId,
    string Signature,
    string User,
    string TransactionId,
    decimal Amount);
