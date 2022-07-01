namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record AgentBalanceQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature);
}
