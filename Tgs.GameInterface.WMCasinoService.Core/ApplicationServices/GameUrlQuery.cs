namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record GameUrlQuery(
	Uri ApiUrl,
	string VendorId,
	string Signature,
	string User,
	string Password,
	int Language);
