using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

internal class CreatePlayerCommandService(
    IGameApiClient client) : ICommandService<CreatePlayerCommand>
{
    public async ValueTask ExecuteAsync(CreatePlayerCommand command, CancellationToken cancellationToken = default)
    {
        var request = new MemberRegisterRequest
        {
            VendorId = command.VendorId,
            Signature = command.Signature,
            Password = command.Password,
            User = command.User,
            Username = command.User
        };
        _ = await client
            .MemberRegisterAsync(command.ApiUrl, request, cancellationToken)
            .ConfigureAwait(false);
    }
}
