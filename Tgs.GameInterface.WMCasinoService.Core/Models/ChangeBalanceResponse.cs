using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models
{
    public record ChangeBalanceResponse
    {
        /// <summary>
        /// 错误代码(0 :表示成功,其他为失败)
        /// </summary>
        /// <value>
        /// 0 该会员交易成功<br />
        /// 901 转点失败<br />
        /// 10410 会员上笔交易未成功，请联系客服人员解锁<br />
        /// 10501 查无此帐号，请检查<br />
        /// 10507 此账号非此代理下线，不能使用此功能<br />
        /// 10801 加扣点不得为零<br />
        /// 10802 加扣点为空，或未设置(money)参数<br />
        /// 10803 加扣点不得为汉字<br />
        /// 10804 不得5秒內重复转帐<br />
        /// 10805 转账失败，该账号余额不足<br />
        /// 10806 转账失败，账户代理已超过信用额度<br />
        /// 10807 转帐失败，该笔单号已存在<br />
        /// 10808 转帐失败,一分钟内转帐次数超过10次,帐号已锁定<br />
        /// 10810 连线异常，交易未成功
        /// </value>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; init; }
        /// <summary>
        /// 错误讯息
        /// </summary>
        [JsonPropertyName("errorMessage")]
        public string ErrorMessage { get; init; } = default!;

        [JsonPropertyName("result")]
        public ChangeBalanceResult Result { get; init; } = default!;
    }
}
