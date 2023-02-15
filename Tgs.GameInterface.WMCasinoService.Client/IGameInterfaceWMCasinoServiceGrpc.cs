using Tgs.GameInterface.WMCasinoService.Protos;

namespace Tgs.GameInterface.WMCasinoService.Client;

public interface IGameInterfaceWMCasinoServiceGrpc
{
    ValueTask CreatePlayerAsync(Uri apiUrl, string vendorId, string signature, string user, string password, string userName, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceFundReply> FundInAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceFundOrderReply> FundOrderAsync(Uri apiUrl, string vendorId, string signature, DateTime? startTime, DateTime? endTime, string? user, string? transactionId, string? gameProviderTransactionId, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceFundReply> FundOutAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceGamePullRecordReply> GamePullRecordAsync(Uri apiUrl, string vendorId, string signature, DateTime startTime, DateTime? endTime, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceGameUnsettleRecordReply> GameUnsettleAsync(Uri apiUrl, string vendorId, string signature, DateTime time, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceGameUrlReply> GameUrlAsync(Uri apiUrl, string vendorId, string signature, string user, string password, int language, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceAgentBalanceReply> GetAgentBalanceAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default);

    ValueTask<GameInterfaceWMCasinoServiceBalanceReply> GetBalanceAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default);

    ValueTask KickAllPlayersAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default);

    ValueTask KickPlayerAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default);
}
