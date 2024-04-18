using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models;

public record GetMemberTradeReportResponse
{
	/// <summary>
	/// 错误代码(0 :表示成功,其他为失败)
	/// </summary>
	/// <value>
	/// 0 该会员操作成功<br/>
	/// 107 操作成功，但未搜寻到数据<br/>
	/// 10501 查无此帐号,请检查<br/>
	/// 10502 帐号名不得为空
	/// </value>
	[JsonPropertyName("errorCode")]
	public int ErrorCode { get; init; }
	/// <summary>
	/// 错误讯息
	/// </summary>
	[JsonPropertyName("errorMessage")]
	public string ErrorMessage { get; init; } = default!;

	[JsonPropertyName("result")]
	public GetMemberTradeReportResult[] Result { get; set; } = default!;
}
