using System.Text.Json.Serialization;

namespace Tgs.GameInterface.WMCasinoService.Core.Models;

public record MemberRegisterResponse
{
    /// <summary>
    /// 错误代码(0:表示成功,其他为失败)
    /// </summary>
    /// <value>
    /// 104 新增会员资料错误,此帐号已被使用!!<br />
    /// 10404 帐号长度过长<br />
    /// 10405 帐号长度过短<br />
    /// 10406 密码长度过短<br />
    /// 10407 密码长度过长<br />
    /// 10409 姓名长度过长<br />
    /// 10502 帐号名不得为空<br />
    /// 10508 密码不得为空<br />
    /// 10509 姓名不得为空<br />
    /// 10419 筹码格式错误(请用逗号隔开)<br />
    /// 10420 筹码个数错误(介于5-10个)<br />
    /// 10421 筹码种类错误<br />
    /// 10422 帐号只接受英文、数字、下划线与@<br />
    /// 10520 上层代理停用或停押
    /// </value>
    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; init; } = default!;
    /// <summary>
    /// 错误讯息
    /// </summary>
    [JsonPropertyName("errorMessage")]
    public string ErrorMessage { get; init; } = default!;

    /// <summary>
    /// Gets the result.
    /// </summary>
    /// <value>
    /// 新增完成
    /// </value>
    [JsonPropertyName("result")]
    public string Result { get; init; } = default!;
}
