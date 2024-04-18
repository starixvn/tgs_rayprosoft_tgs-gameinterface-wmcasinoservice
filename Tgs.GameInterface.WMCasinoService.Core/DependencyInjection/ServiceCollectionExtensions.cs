using System.Reflection;
using Tgs.GameInterface.WMCasinoService.Core;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddGameInterfaceCore(this IServiceCollection services)
		=> services
		.AddHttpClient<IGameApiClient, GameApiClient>()
		.Services
		.RegisterAllServiceUtilityTypes(
			Assembly.GetExecutingAssembly(),
			ServiceLifetime.Transient);
}
