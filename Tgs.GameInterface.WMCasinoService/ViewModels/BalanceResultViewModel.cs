namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record BalanceResultViewModel(
    bool Success,
    bool IsPlayerExist,
    string? Message,
    decimal? Balance);
