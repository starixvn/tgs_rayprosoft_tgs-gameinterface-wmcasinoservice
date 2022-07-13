namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record AgentBalanceResult(
        bool Success,
        string? Message,
        decimal? Balance);
}
