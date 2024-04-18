using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class GameUrlGetService(
	IGameApiClient client) : IGetService<GameUrlQuery, GameUrlResult>
{
	public async ValueTask<GameUrlResult> GetAsync(GameUrlQuery query, CancellationToken cancellationToken = default)
	{
		var request = new SigninGameRequest
		{
			VendorId = query.VendorId,
			Signature = query.Signature,
			User = query.User,
			Password = query.Password,
			Lang = query.Language
		};
		var result = await client.SigninGameAsync(
			query.ApiUrl,
			request,
			cancellationToken).ConfigureAwait(false);

		return new GameUrlResult(
			result.Success,
			result.IsPlayerExist,
			result.Message,
			result.GameUrl);
	}
}
