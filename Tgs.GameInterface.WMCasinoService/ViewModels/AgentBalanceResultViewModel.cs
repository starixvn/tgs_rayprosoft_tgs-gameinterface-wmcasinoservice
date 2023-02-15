namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record AgentBalanceResultViewModel(
    bool Success,
    string? Message,
    decimal? Balance);
