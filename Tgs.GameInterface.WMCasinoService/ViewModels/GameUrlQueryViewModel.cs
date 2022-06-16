namespace Tgs.GameInterface.WMCasinoService.ViewModels
{
    public record GameUrlQueryViewModel
    {
        public Uri ApiUrl { get; init; } = default!;
        public string VendorId { get; init; } = default!;
        public string Signature { get; init; } = default!;
        public string PlayerCode { get; init; } = default!;
        public string Password { get; init; } = default!;
        /// <summary>
        /// 使用语言
        /// </summary>
        /// <value>
        /// 0 简体中文<br/>
        /// 1 英文<br/>
        /// 2 泰文<br/>
        /// 3 越文<br/>
        /// 4 日文<br/>
        /// 5 韩文<br/>
        /// 6 印度文<br/>
        /// 7 马来西亚文<br/>
        /// 8 印尼文<br/>
        /// 9 繁体中文<br/>
        /// 10 西文
        /// </value>
        public int Language { get; init; } = 0;
    };
}
