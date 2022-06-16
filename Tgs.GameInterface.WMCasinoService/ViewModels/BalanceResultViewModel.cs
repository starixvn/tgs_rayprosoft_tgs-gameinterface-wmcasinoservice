namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record BalanceResultViewModel(
        bool IsPlayerExist,
        decimal? Balance);
}
