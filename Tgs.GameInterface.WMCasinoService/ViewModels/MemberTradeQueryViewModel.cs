namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record MemberTradeQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime? StartTime,
        DateTime? EndTime,
        string? User,
        string? TransactionId,
        string? GameProviderTransactionId);
}
