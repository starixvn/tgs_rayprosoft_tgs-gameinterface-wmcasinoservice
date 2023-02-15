namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record CreatePlayerViewModel(
    Uri ApiUrl,
    string VendorId,
    string Signature,
    string User,
    string Password,
    string UserName);
