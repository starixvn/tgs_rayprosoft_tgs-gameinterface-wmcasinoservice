namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record FundOutResult(
        string? TransactionId,
        string? GameProviderTransactionId,
        decimal? CurrentBalance);
}
