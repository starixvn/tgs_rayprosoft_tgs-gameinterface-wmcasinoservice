namespace Tgs.GameInterface.WMCasinoService.Core.Models;

public record SigninGameRequest
{
    /// <summary>
    /// 代理商(aid)
    /// </summary>
    public string VendorId { get; init; } = default!;
    /// <summary>
    /// 代理商标识符
    /// </summary>
    public string Signature { get; init; } = default!;
    /// <summary>
    /// 帐号
    /// </summary>
    public string User { get; init; } = default!;
    /// <summary>
    /// 密碼
    /// </summary>
    public string Password { get; init; } = default!;
    /// <summary>
    /// 使用语言
    /// 0或空值 为简体中文
    /// 1为英文
    /// 2为泰文
    /// 3为越文
    /// 4为日文
    /// 5为韩文
    /// 6为印度文
    /// 7为马来西亚文
    /// 8为印尼文
    /// 9为繁体中文
    /// 10为西文
    /// </summary>
    public int Lang { get; init; }
}
