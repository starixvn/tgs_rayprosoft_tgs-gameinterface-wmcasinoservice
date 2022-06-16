namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record MemberTradeQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTime? StartTime,
        DateTime? EndTime,
        string? PlayerCode,
        string? TransactionId,
        string? GameProviderTransactionId);
}
