namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record MemberTradeQueryViewModel(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime? StartTime,
        DateTime? EndTime,
        string? PlayerCode,
        string? TransactionId,
        string? GameProviderTransactionId);
}
