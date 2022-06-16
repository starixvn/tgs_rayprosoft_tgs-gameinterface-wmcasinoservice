using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class CreatePlayerCommandService : ICommandService<CreatePlayerCommand>
    {
        private readonly IGameApiClient _client;

        public CreatePlayerCommandService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask ExecuteAsync(CreatePlayerCommand command, CancellationToken cancellationToken = default)
        {
            var request = new MemberRegisterRequest
            {
                VendorId = command.VendorId,
                Signature = command.Signature,
                Password = command.Password,
                User = command.PlayerCode,
                Username = command.PlayerCode
            };
            _ = await _client
                .MemberRegisterAsync(command.ApiUrl, request, cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
