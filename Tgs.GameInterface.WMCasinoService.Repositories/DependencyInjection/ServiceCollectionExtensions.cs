using Tgs.GameInterface.WMCasinoService.Core;
using Tgs.GameInterface.WMCasinoService.Repositories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddWMCasinoServiceRepositories(this IServiceCollection services)
            => services
                .AddHttpClient<IGameApiClient, GameApiClient>()
                .Services;
    }
}
