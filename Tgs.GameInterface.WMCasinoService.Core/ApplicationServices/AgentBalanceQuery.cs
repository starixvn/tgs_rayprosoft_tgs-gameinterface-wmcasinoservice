namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record AgentBalanceQuery(
    Uri ApiUrl,
    string VendorId,
    string Signature);
