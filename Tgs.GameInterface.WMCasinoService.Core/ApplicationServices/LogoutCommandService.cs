using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class LogoutCommandService : ICommandService<LogoutCommand>
    {
        private readonly IGameApiClient _client;

        public LogoutCommandService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask ExecuteAsync(LogoutCommand command, CancellationToken cancellationToken = default)
        {
            var request = new LogoutGameRequest
            {
                VendorId = command.VendorId,
                Signature = command.Signature,
                User = command.User
            };

            _ = await _client.LogoutGameAsync(
                command.ApiUrl,
                request,
                cancellationToken).ConfigureAwait(false);
        }
    }
}
