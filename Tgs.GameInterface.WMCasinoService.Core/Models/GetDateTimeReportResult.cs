using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record GetDateTimeReportResult
    {
        /// <summary>
        /// 账号
        /// </summary>
        /// <value>
        /// playerCode
        /// </value>
        [JsonPropertyName("user")]
        public string User { get; init; } = default!;
        /// <summary>
        /// 注单号
        /// </summary>
        /// <value>
        /// 179228694
        /// </value>
        [JsonPropertyName("betId")]
        public string BetId { get; init; } = default!;
        /// <summary>
        /// 下注時間
        /// </summary>
        /// <value>
        /// 2022-06-13 09:55:10
        /// </value>
        [JsonPropertyName("betTime")]
        public string BetTime { get; init; } = default!;
        /// <summary>
        /// 下注前金额
        /// </summary>
        /// <value>
        /// 100.0000
        /// </value>
        [JsonPropertyName("beforeCash")]
        public string BeforeCash { get; init; } = default!;
        /// <summary>
        /// 下注金额
        /// </summary>
        /// <value>
        /// 10.0000
        /// </value>
        [JsonPropertyName("bet")]
        public string Bet { get; init; } = default!;
        /// <summary>
        /// 有效下注
        /// </summary>
        /// <value>
        /// 10.0000
        /// </value>
        [JsonPropertyName("validbet")]
        public string Validbet { get; init; } = default!;
        /// <summary>
        /// 退水金额
        /// </summary>
        /// <value>
        /// 0.0000
        /// </value>
        [JsonPropertyName("water")]
        public string Water { get; init; } = default!;
        /// <summary>
        /// 下注结果
        /// </summary>
        /// <value>
        /// 10.0000
        /// </value>
        [JsonPropertyName("result")]
        public string Result { get; init; } = default!;
        /// <summary>
        /// 下注代碼
        /// </summary>
        /// <value>
        /// Player
        /// </value>
        [JsonPropertyName("betCode")]
        public string BetCode { get; init; } = default!;
        /// <summary>
        /// 下注退水金额
        /// </summary>
        /// <value>
        /// 10.0000
        /// </value>
        [JsonPropertyName("waterbet")]
        public string Waterbet { get; init; } = default!;
        /// <summary>
        /// 输赢金额
        /// </summary>
        /// <value>
        /// 10.0000
        /// </value>
        [JsonPropertyName("winLoss")]
        public string WinLoss { get; init; } = default!;
        /// <summary>
        /// IP
        /// </summary>
        [JsonPropertyName("ip")]
        public string Ip { get; init; } = default!;
        /// <summary>
        /// 游戏类别编号
        /// </summary>
        /// <value>
        /// 101:百家乐,
        /// 102:龙虎,
        /// 103:轮盘,
        /// 104:骰宝,
        /// 105:牛牛,
        /// 106:三公,
        /// 107:番摊,
        /// 108:色碟,
        /// 110:鱼虾蟹,
        /// 111:炸金花,
        /// 112:温州牌九,
        /// 113:二八杠,
        /// 128:安達巴哈
        /// </value>
        [JsonPropertyName("gid")]
        public string Gid { get; init; } = default!;
        /// <summary>
        /// 场次编号
        /// </summary>
        /// <value>
        /// 113053285
        /// </value>
        [JsonPropertyName("event")]
        public string Event { get; init; } = default!;
        /// <summary>
        /// 子场次编号
        /// </summary>
        /// <value>
        /// 5
        /// </value>
        [JsonPropertyName("eventChild")]
        public string EventChild { get; init; } = default!;
        /// <summary>
        /// 场次编号
        /// </summary>
        /// <value>
        /// 113053285
        /// </value>
        [JsonPropertyName("round")]
        public string Round { get; init; } = default!;
        /// <summary>
        /// 子场次编号
        /// </summary>
        /// <value>
        /// 5
        /// </value>
        [JsonPropertyName("subround")]
        public string Subround { get; init; } = default!;
        /// <summary>
        /// 桌台编号
        /// </summary>
        /// <value>
        /// 112
        /// </value>
        [JsonPropertyName("tableId")]
        public string TableId { get; init; } = default!;
        /// <summary>
        /// 0:一般, 1:免佣
        /// </summary>
        /// <value>
        /// 0
        /// </value>
        [JsonPropertyName("commission")]
        public string Commission { get; init; } = default!;
        /// <summary>
        /// 结算时间
        /// </summary>
        /// <value>
        /// 2022-06-13 09:55:29
        /// </value>
        [JsonPropertyName("settime")]
        public string Settime { get; init; } = default!;
        /// <summary>
        /// Y:有重对, N:非重对
        /// </summary>
        /// <value>
        /// N
        /// </value>
        [JsonPropertyName("reset")]
        public string Reset { get; init; } = default!;
        /// <summary>
        /// 下注内容 ex:"Banker"
        /// </summary>
        /// <value>
        /// 闲
        /// </value>
        [JsonPropertyName("betResult")]
        public string BetResult { get; init; } = default!;
        /// <summary>
        /// 牌型 ex:庄:♦3♦3 闲:♥9♣10<br />
        /// 游戏结果取消会显示:The council canceled
        /// </summary>
        /// <value>
        /// 庄:♣3♦Q闲:♣A♥9♠8
        /// </value>
        [JsonPropertyName("gameResult")]
        public string GameResult { get; init; } = default!;
        /// <summary>
        /// 游戏名称 ex:百家乐
        /// </summary>
        /// <value>
        /// 百家乐
        /// </value>
        [JsonPropertyName("gname")]
        public string Gname { get; init; } = default!;
    }
}
