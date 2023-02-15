namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record FundResultViewModel(
    bool Success,
    string? Message,
    string? GameProviderTransactionId,
    string? TransactionId,
    decimal? CurrentBalance);
