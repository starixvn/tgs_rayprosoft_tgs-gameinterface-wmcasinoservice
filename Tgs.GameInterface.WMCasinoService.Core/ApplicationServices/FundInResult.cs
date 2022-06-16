namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record FundInResult(string GameProviderTransactionId, string? TransactionId, decimal? CurrentBalance);
}
