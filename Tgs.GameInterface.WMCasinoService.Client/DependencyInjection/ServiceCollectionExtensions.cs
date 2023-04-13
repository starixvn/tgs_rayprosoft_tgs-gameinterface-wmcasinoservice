using Microsoft.Extensions.Options;
using Tgs.GameInterface.WMCasinoService.Client;
using Tgs.GameInterface.WMCasinoService.Protos;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGameInterfaceWMCasinoServiceClient(
        this IServiceCollection services,
        Action<GameInterfaceWMCasinoServiceClientOptions, IServiceProvider> configure)
        => services
            .AddOptions<GameInterfaceWMCasinoServiceClientOptions>()
            .Configure(configure)
            .Services
            .AddGrpcClient<GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceClient>((sp, configureClient) =>
            {
                var options = sp.GetRequiredService<IOptions<GameInterfaceWMCasinoServiceClientOptions>>().Value;
                configureClient.Address = options.BaseUrl;
            })
            .ConfigureChannel((sp, configureChannel) =>
            {
                var handler = new SocketsHttpHandler
                {
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                    EnableMultipleHttp2Connections = true
                };
                configureChannel.HttpHandler = handler;
            })
            .AddInterceptor<LoggingInterceptor>()
            .Services
            .AddSingleton<DiagnosticsInterceptorOptions>()
            .AddSingleton<LoggingInterceptor>()
            .AddTransient<IGameInterfaceWMCasinoServiceClient, GameInterfaceWMCasinoServiceClient>();
}
