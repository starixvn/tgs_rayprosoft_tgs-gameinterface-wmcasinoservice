namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record GameUrlResultViewModel(
	bool Success,
	bool IsPlayerExist,
	string? Message = null,
	string? GameUrl = null);
