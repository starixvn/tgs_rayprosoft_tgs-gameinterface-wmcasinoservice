using Tgs.GameInterface.WMCasinoService.Protos;

namespace Tgs.GameInterface.WMCasinoService.Client;

public interface IGameInterfaceWMCasinoServiceClient
{
    ValueTask<string> CheckingServiceAsync(string machineName, CancellationToken cancellationToken = default);

    ValueTask CreatePlayerAsync(Uri apiUrl, string vendorId, string signature, string user, string password, string userName, CancellationToken cancellationToken = default);

    ValueTask<FundReply> FundInAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default);

    ValueTask<FundOrderReply> FundOrderAsync(Uri apiUrl, string vendorId, string signature, DateTime? startTime, DateTime? endTime, string? user, string? transactionId, string? gameProviderTransactionId, CancellationToken cancellationToken = default);

    ValueTask<FundReply> FundOutAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default);

    ValueTask<GamePullRecordReply> GamePullRecordAsync(Uri apiUrl, string vendorId, string signature, DateTime startTime, DateTime? endTime, CancellationToken cancellationToken = default);

    ValueTask<GameUnsettleRecordReply> GameUnsettleAsync(Uri apiUrl, string vendorId, string signature, DateTime time, CancellationToken cancellationToken = default);

    ValueTask<GameUrlReply> GameUrlAsync(Uri apiUrl, string vendorId, string signature, string user, string password, int language, CancellationToken cancellationToken = default);

    ValueTask<AgentBalanceReply> GetAgentBalanceAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default);

    ValueTask<BalanceReply> GetBalanceAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default);

    ValueTask KickAllPlayersAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default);

    ValueTask KickPlayerAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default);
}
