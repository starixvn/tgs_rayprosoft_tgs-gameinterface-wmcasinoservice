using System.Diagnostics;

namespace Tgs.GameInterface.WMCasinoService;

public class DiagnosticsInterceptorOptions
{
	public ActivitySource InterceptorActivitySource { get; set; } =
		new ActivitySource($"Tgs.GameInterface.WMCasinoService.DiagnosticsInterceptor");
}
