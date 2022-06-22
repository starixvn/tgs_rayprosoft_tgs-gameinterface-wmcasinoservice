namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record FundInResult(
        bool Success,
        string? Message,
        string? GameProviderTransactionId,
        string? TransactionId,
        decimal? CurrentBalance);
}
