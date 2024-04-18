namespace Tgs.GameInterface.WMCasinoService.ViewModels;

public record GamePullRecordQueryViewModel(
	Uri ApiUrl,
	string VendorId,
	string Signature,
	DateTimeOffset StartTime,
	DateTimeOffset EndTime);
