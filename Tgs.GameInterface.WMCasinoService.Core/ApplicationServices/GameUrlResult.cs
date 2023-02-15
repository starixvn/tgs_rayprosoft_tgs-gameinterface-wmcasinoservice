namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record GameUrlResult(
    bool Success,
    bool IsPlayerExist,
    string? Message = null,
    string? GameUrl = null);
