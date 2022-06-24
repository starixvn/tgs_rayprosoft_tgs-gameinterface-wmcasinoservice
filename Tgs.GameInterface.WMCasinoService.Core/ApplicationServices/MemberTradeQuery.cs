namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    public record MemberTradeQuery(
        Uri ApiUrl,
        string VendorId,
        string Signature,
        DateTimeOffset? StartTime,
        DateTimeOffset? EndTime,
        string? User,
        string? TransactionId,
        string? GameProviderTransactionId);
}
