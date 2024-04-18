using Google.Protobuf.WellKnownTypes;
using Tgs.GameInterface.WMCasinoService.Protos;

namespace Tgs.GameInterface.WMCasinoService.Client;

internal class GameInterfaceWMCasinoServiceClient(GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceClient client) : IGameInterfaceWMCasinoServiceClient
{
	public async ValueTask<string> CheckingServiceAsync(string machineName, CancellationToken cancellationToken = default)
	{
		var request = new CheckingServiceRequest
		{
			MachineName = machineName,
		};

		var reply = await client
			.CheckingServiceAsync(
				request: request,
				cancellationToken: cancellationToken)
			.ConfigureAwait(false);

		return reply.MachineName;
	}

	public async ValueTask CreatePlayerAsync(Uri apiUrl, string vendorId, string signature, string user, string password, string userName, CancellationToken cancellationToken = default)
	{
		_ = await client
			.CreatePlayerAsync(new CreatePlayerRequest
			{
				ApiUrl = apiUrl.ToString(),
				VendorId = vendorId,
				Signature = signature,
				User = user,
				Password = password,
				UserName = userName
			},
				cancellationToken: cancellationToken)
			.ConfigureAwait(false);
	}

	public async ValueTask<FundReply> FundInAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default)
		=> await client.FundInAsync(new FundRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			User = user,
			TransactionId = transactionId,
			Amount = amount
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<FundOrderReply> FundOrderAsync(Uri apiUrl, string vendorId, string signature, DateTime? startTime, DateTime? endTime, string? user, string? transactionId, string? gameProviderTransactionId, CancellationToken cancellationToken = default)
		=> await client.FundOrderAsync(new FundOrderRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			StartTime = startTime?.ToTimestamp(),
			EndTime = endTime?.ToTimestamp()
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<FundReply> FundOutAsync(Uri apiUrl, string vendorId, string signature, string user, string transactionId, decimal amount, CancellationToken cancellationToken = default)
		=> await client.FundOutAsync(new FundRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			User = user,
			TransactionId = transactionId,
			Amount = amount
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<GamePullRecordReply> GamePullRecordAsync(Uri apiUrl, string vendorId, string signature, DateTime startTime, DateTime? endTime, CancellationToken cancellationToken = default)
	=> await client.GamePullRecordAsync(new GamePullRecordeRequest
	{
		ApiUrl = apiUrl.ToString(),
		VendorId = vendorId,
		Signature = signature,
		StartTime = startTime.ToTimestamp(),
		EndTime = endTime?.ToTimestamp()
	}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<GameUnsettleRecordReply> GameUnsettleAsync(Uri apiUrl, string vendorId, string signature, DateTime time, CancellationToken cancellationToken = default)
		=> await client.GameUnsettleAsync(new GameUnsettleRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			Time = time.ToTimestamp(),
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<GameUrlReply> GameUrlAsync(Uri apiUrl, string vendorId, string signature, string user, string password, int language, CancellationToken cancellationToken = default)
		=> await client.GameUrlAsync(new GameUrlRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			User = user,
			Password = password,
			Language = language
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<AgentBalanceReply> GetAgentBalanceAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default)
		=> await client.GetAgentBalanceAsync(new AgentBalanceRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask<BalanceReply> GetBalanceAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default)
				=> await client.GetBalanceAsync(new BalanceRequest
				{
					ApiUrl = apiUrl.ToString(),
					VendorId = vendorId,
					Signature = signature,
					User = user
				}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask KickAllPlayersAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default)
		=> await client.KickAllPlayerAsync(new KickAllPlayersRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature
		}, cancellationToken: cancellationToken).ConfigureAwait(false);

	public async ValueTask KickPlayerAsync(Uri apiUrl, string vendorId, string signature, string user, CancellationToken cancellationToken = default)
		=> await client.KickPlayerAsync(new KickPlayerRequest
		{
			ApiUrl = apiUrl.ToString(),
			VendorId = vendorId,
			Signature = signature,
			User = user
		}, cancellationToken: cancellationToken).ConfigureAwait(false);
}
