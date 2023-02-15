namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record FundOutResult(
    bool Success,
    string? Message,
    string? TransactionId,
    string? GameProviderTransactionId,
    decimal? CurrentBalance);
