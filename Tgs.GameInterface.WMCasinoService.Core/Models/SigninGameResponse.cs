namespace Tgs.GameInterface.WMCasinoService.Core.Models;

public record SigninGameResponse
{
    public bool Success { get; init; }
    public int ErrorCode { get; init; }

    public string? Message { get; init; }
    public string? GameUrl { get; init; }
    public bool IsPlayerExist { get; init; }
}
