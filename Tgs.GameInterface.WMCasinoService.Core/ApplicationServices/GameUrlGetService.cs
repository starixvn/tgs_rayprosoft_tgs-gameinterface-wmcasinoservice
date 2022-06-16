using Tgs.GameInterface.WMCasinoService.Core.Models;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices
{
    internal class GameUrlGetService : IGetService<GameUrlQuery, GameUrlResult>
    {
        private readonly IGameApiClient _client;

        public GameUrlGetService(IGameApiClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async ValueTask<GameUrlResult> GetAsync(GameUrlQuery query, CancellationToken cancellationToken = default)
        {
            var request = new SigninGameRequest
            {
                VendorId = query.VendorId,
                Signature = query.Signature,
                User = query.PlayerCode,
                Password = query.Password,
                Lang = query.Language
            };
            var result = await _client.SigninGameAsync(
                query.ApiUrl,
                request,
                cancellationToken).ConfigureAwait(false);

            return new GameUrlResult(result.Result);
        }
    }
}
