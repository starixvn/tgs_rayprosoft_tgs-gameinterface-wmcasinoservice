using Google.Protobuf.WellKnownTypes;
using Tgs.GameInterface.WMCasinoService.Protos;

namespace Tgs.GameInterface.WMCasinoService.Client
{
    internal class GameInterfaceWMCasinoServiceGrpc : IGameInterfaceWMCasinoServiceGrpc
    {
        private readonly GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceClient _client;

        public GameInterfaceWMCasinoServiceGrpc(GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask CreatePlayerAsync(Uri apiUrl, string vendorId, string signature, string playerCode, string password, string userName, CancellationToken cancellationToken = default)
        {
            _ = await _client
                .CreatePlayerAsync(new GameInterfaceWMCasinoServiceCreatePlayerRequest
                {
                    ApiUrl = apiUrl.ToString(),
                    VendorId = vendorId,
                    Signature = signature,
                    PlayerCode = playerCode,
                    Password = password,
                    UserName = userName
                },
                    cancellationToken: cancellationToken)
                .ConfigureAwait(false);
        }

        public async ValueTask<GameInterfaceWMCasinoServiceFundReply> FundInAsync(Uri apiUrl, string vendorId, string signature, string playerCode, string transactionId, decimal amount, CancellationToken cancellationToken = default)
            => await _client.FundInAsync(new GameInterfaceWMCasinoServiceFundRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                PlayerCode = playerCode,
                TransactionId = transactionId,
                Amount = amount
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public ValueTask<GameInterfaceWMCasinoServiceFundOrderReply> FundOrderAsync(Uri apiUrl, string vendorId, string signature, DateTime? startTime, DateTime? endTime, string? playerCode, string? transactionId, string? gameProviderTransactionId, CancellationToken cancellationToken = default) => throw new NotImplementedException();

        public async ValueTask<GameInterfaceWMCasinoServiceFundReply> FundOutAsync(Uri apiUrl, string vendorId, string signature, string playerCode, string transactionId, decimal amount, CancellationToken cancellationToken = default)
            => await _client.FundOutAsync(new GameInterfaceWMCasinoServiceFundRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                PlayerCode = playerCode,
                TransactionId = transactionId,
                Amount = amount
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask<GameInterfaceWMCasinoServiceGamePullRecordReply> GamePullRecordAsync(Uri apiUrl, string vendorId, string signature, DateTime startTime, DateTime? endTime, string? playerCode, CancellationToken cancellationToken = default)
        => await _client.GamePullRecordAsync(new GameInterfaceWMCasinoServiceGamePullRecordeRequest
        {
            ApiUrl = apiUrl.ToString(),
            VendorId = vendorId,
            Signature = signature,
            StartTime = startTime.ToTimestamp(),
            EndTime = endTime?.ToTimestamp()
        }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask<GameInterfaceWMCasinoServiceGameUnsettleRecordReply> GameUnsettleAsync(Uri apiUrl, string vendorId, string signature, DateTime time, CancellationToken cancellationToken = default)
            => await _client.GameUnsettleAsync(new GameInterfaceWMCasinoServiceGameUnsettleRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                Time = time.ToTimestamp(),
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask<GameInterfaceWMCasinoServiceGameUrlReply> GameUrlAsync(Uri apiUrl, string vendorId, string signature, string playerCode, string password, int language, CancellationToken cancellationToken = default)
            => await _client.GameUrlAsync(new GameInterfaceWMCasinoServiceGameUrlRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                PlayerCode = playerCode,
                Password = password,
                Language = language
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask<GameInterfaceWMCasinoServiceBalanceReply> GetBalanceAsync(Uri apiUrl, string vendorId, string signature, string playerCode, CancellationToken cancellationToken = default)
            => await _client.GetBalanceAsync(new GameInterfaceWMCasinoServiceBalanceRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                PlayerCode = playerCode
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask KickAllPlayersAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default)
            => await _client.KickAllPlayerAsync(new GameInterfaceWMCasinoServiceKickAllPlayersRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature
            }, cancellationToken: cancellationToken).ConfigureAwait(false);

        public async ValueTask KickPlayerAsync(Uri apiUrl, string vendorId, string signature, string playerCode, CancellationToken cancellationToken = default)
            => await _client.KickPlayerAsync(new GameInterfaceWMCasinoServiceKickPlayerRequest
            {
                ApiUrl = apiUrl.ToString(),
                VendorId = vendorId,
                Signature = signature,
                PlayerCode = playerCode
            }, cancellationToken: cancellationToken).ConfigureAwait(false);
    }
}
