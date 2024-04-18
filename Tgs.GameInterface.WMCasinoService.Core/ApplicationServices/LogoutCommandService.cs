using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class LogoutCommandService(
	IGameApiClient client) : ICommandService<LogoutCommand>
{
	public async ValueTask ExecuteAsync(LogoutCommand command, CancellationToken cancellationToken = default)
	{
		var request = new LogoutGameRequest
		{
			VendorId = command.VendorId,
			Signature = command.Signature,
			User = command.User
		};

		_ = await client.LogoutGameAsync(
			command.ApiUrl,
			request,
			cancellationToken).ConfigureAwait(false);
	}
}
