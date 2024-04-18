namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record BalanceResult(
	bool Success,
	bool IsPlayerExist,
	string? Message,
	decimal? Balance);
