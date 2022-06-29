namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetAgentBalanceResponse
    {
        public int ErrorCode { get; init; }

        public string? Message { get; init; }

        public decimal? Balance { get; init; }
        public bool Success { get; init; }
    }
}
