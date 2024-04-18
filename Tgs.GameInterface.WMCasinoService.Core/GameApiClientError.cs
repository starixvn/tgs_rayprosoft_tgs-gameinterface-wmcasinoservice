using System.ComponentModel;

namespace Tgs.GameInterface.WMCasinoService.Core;

public enum GameApiClientError
{
	/// <summary>
	/// 该会员操作成功
	/// </summary>
	[Description("该会员操作成功")]
	Success = 0,

	/// <summary>
	/// 代理商ID与识别码格式错误
	/// </summary>
	[Description("代理商ID与识别码格式错误")]
	VendorIdAndSignatureFormatWrong = 103,

	/// <summary>
	/// 查无此函数
	/// </summary>
	[Description("查无此函数")]
	QueryNoFunction = 900,

	/// <summary>
	/// 维护中
	/// </summary>
	[Description("维护中")]
	Maintenance = 911,

	/// <summary>
	/// 此功能仅能查询一天内的报表，您已超过上限
	/// </summary>
	[Description("此功能仅能查询一天内的报表，您已超过上限")]
	OverReportQueryLimit = 10201,

	/// <summary>
	/// 时间戳异常,超过30秒
	/// </summary>
	[Description("时间戳异常,超过30秒")]
	UnusualTimestamp = 10202,

	/// <summary>
	/// 代理商ID为空,请检查(vendorId)
	/// </summary>
	[Description("代理商ID为空,请检查(vendorId)")]
	EmptyVendorId = 10301,

	/// <summary>
	/// 没有这个代理商ID
	/// </summary>
	[Description("没有这个代理商ID")]
	NonVendorId = 10302,

	/// <summary>
	/// 有此代理商ID,但代理商代码(signature)错误
	/// </summary>
	[Description("有此代理商ID,但代理商代码(signature)错误")]
	SignatureError = 10303,

	/// <summary>
	/// 代理商代码(signature)为空
	/// </summary>
	[Description("代理商代码(signature)为空")]
	SignatureEmpty = 10304,

	/// <summary>
	/// 請於30秒後再試
	/// </summary>
	[Description("請於30秒後再試")]
	TryAfter30Sec = 10411,

	/// <summary>
	/// 請於10秒後再試
	/// </summary>
	[Description("請於10秒後再試")]
	TryAfter10Sec = 10418,

	/// <summary>
	/// 查无此帐号,请检查
	/// </summary>
	[Description("查无此帐号,请检查")]
	QueryNoUser = 10501,

	/// <summary>
	/// 帐号名不得为空
	/// </summary>
	[Description("帐号名不得为空")]
	EmptyUserName = 10502,

	/// <summary>
	/// 此帐号的密码错误
	/// </summary>
	[Description("此帐号的密码错误")]
	WrongUserPassword = 10504,

	/// <summary>
	/// 此帐号已被停用
	/// </summary>
	[Description("此帐号已被停用")]
	UserBlocked = 10505,

	/// <summary>
	/// 此账号非此代理下线,不能使用此功能
	/// </summary>
	[Description("此账号非此代理下线,不能使用此功能")]
	UserNotBelongToAgent = 10507,

	/// <summary>
	/// 帐号密码格式错误
	/// </summary>
	[Description("帐号密码格式错误")]
	UserPasswordFormatWrong = 10512,

	/// <summary>
	/// 限額未開放，請檢查
	/// </summary>
	[Description("限額未開放，請檢查")]
	UserLimitNotOpen = 10601
}
