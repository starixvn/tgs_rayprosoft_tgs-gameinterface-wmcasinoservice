namespace Tgs.GameInterface.WMCasinoService.Client;

public class GameInterfaceWMCasinoServiceClientOptions
{
	public Uri BaseUrl { get; set; } = default!;
	public TimeSpan GrpcConnectTimeout { get; set; } = TimeSpan.FromSeconds(5);
}
