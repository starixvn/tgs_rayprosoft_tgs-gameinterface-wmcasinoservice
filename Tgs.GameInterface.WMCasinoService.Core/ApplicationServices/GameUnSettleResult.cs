namespace Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;

public record GameUnSettleResult
{
	/// <summary>
	/// 账号
	/// </summary>
	/// <value>
	/// wmtf_{playerCode}_{brandCode}
	/// </value>
	public string User { get; init; } = default!;
	/// <summary>
	/// 注单号
	/// </summary>
	public string BetId { get; init; } = default!;
	/// <summary>
	/// 游戏类别编号<br/>
	/// 101:百家乐,<br/>
	/// 102:龙虎,<br/>
	/// 103:轮盘,<br/>
	/// 104:骰宝,<br/>
	/// 105:牛牛,<br/>
	/// 106:三公,<br/>
	/// 107:番摊,<br/>
	/// 108:色碟,<br/>
	/// 110:鱼虾蟹,<br/>
	/// 111:炸金花,<br/>
	/// 112:温州牌九,<br/>
	/// 113:二八杠,<br/>
	/// 128:安達巴哈
	/// </summary>
	/// <value>
	/// The gid.
	/// </value>
	public string Gid { get; init; } = default!;
	/// <summary>
	/// 场次编号
	/// </summary>
	public string Event { get; init; } = default!;
	/// <summary>
	/// 子场次编号
	/// </summary>
	public string EventChild { get; init; } = default!;
	/// <summary>
	/// 下注時間
	/// </summary>
	public DateTimeOffset BetTime { get; init; }
	/// <summary>
	/// 下注内容 ex:"Banker"
	/// </summary>
	public string BetResult { get; init; } = default!;
	/// <summary>
	/// 下注金额
	/// </summary>
	public decimal Bet { get; init; }
	/// <summary>
	/// 桌台编号
	/// </summary>
	public string TableId { get; init; } = default!;
	/// <summary>
	/// 0:一般, 1:免佣
	/// </summary>
	public string Commission { get; init; } = default!;
	/// <summary>
	/// 场次编号
	/// </summary>
	public string Round { get; init; } = default!;
	/// <summary>
	/// 子场次编号
	/// </summary>
	public string Subround { get; init; } = default!;
	/// <summary>
	/// 游戏名称 ex:百家乐
	/// </summary>
	public string Gname { get; init; } = default!;
	/// <summary>
	/// The additional data
	/// </summary>
	public string AdditionalData { get; init; } = default!;
}
