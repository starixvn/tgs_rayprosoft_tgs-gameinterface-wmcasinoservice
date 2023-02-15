using System.Diagnostics;

namespace Tgs.GameInterface.WMCasinoService.Client;

public class DiagnosticsInterceptorOptions
{
    public ActivitySource InterceptorActivitySource { get; set; } =
        new ActivitySource($"Tgs.GameInterface.WMCasinoService.Client.DiagnosticsInterceptor");
}
