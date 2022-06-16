namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record FundResultViewModel(
        string? TransactionId,
        string? GameProviderTransactionId,
        decimal? CurrentBalance);
}
