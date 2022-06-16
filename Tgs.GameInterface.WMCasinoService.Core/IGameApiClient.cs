using Tgs.GameInterface.WMCasinoService.Core.Models;

namespace Tgs.GameInterface.WMCasinoService.Core
{
    public interface IGameApiClient
    {
        /// <summary>
        /// 加扣點 (用於從網站帳號存入額度到wm遊戲帳號, 或者從wm遊戲帳號提取額度到網站帳號。)
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<ChangeBalanceResponse> ChangeBalanceAsync(Uri apiUrl, ChangeBalanceRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 取餘額 (查詢該會員在利新(WM)平台裡還剩多少餘額。)
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<GetBalanceResponse> GetBalanceAsync(Uri apiUrl, GetBalanceRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 遊戲紀錄報表 (查詢該玩家或者代理旗下會員時間內的遊戲紀錄。)<br/>
        /// 該功能搜尋起始時間至搜尋結束時間不能多於一天。<br/>
        /// 該功能搜尋時間至秒數。例:20170809130101 (2017-08-09 13:01:01)<br/>
        /// 報表查詢需間隔30秒，未搜尋到數據需間隔10秒。<br/>
        /// 請注意：報表資料只保留60天。
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<GetDateTimeReportResponse> GetDateTimeReportAsync(Uri apiUrl, GetDateTimeReportRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 交易紀錄報表 (因網路環境問題(封包遺失，封包阻塞)，或其他不可違之因素可能在進行加扣點时導致失敗，而貴公司有寫入此單號，故做此功能以便查詢爭議的單據。)<br/>
        /// 報表查詢需間隔30秒，未搜尋到數據需間隔10秒。<br/>
        /// 請注意：報表資料只保留60天。
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<GetMemberTradeReportResponse> GetMemberTradeReportAsync(Uri apiUrl, GetMemberTradeReportRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 未結算單查詢 (查詢未結算注單。)<br/>
        /// 報表查詢需間隔30秒，未搜尋到數據需間隔10秒。<br/>
        /// 請注意：報表資料只保留60天。
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<GetUnsettleReportResponse> GetUnsettleReportAsync(Uri apiUrl, GetUnsettleReportRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 連接API測試 (首先使用測試頁面來測試是否接通渠道)
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="signature">The signature.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<HelloResponse> HelloAsync(Uri apiUrl, string vendorId, string signature, CancellationToken cancellationToken = default);

        /// <summary>
        /// 登出遊戲 (將會員登出遊戲。)
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<LogoutGameResponse> LogoutGameAsync(Uri apiUrl, LogoutGameRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 註冊 (增加此代理商(vendorId)的下線帳號)<br/>
        /// user在wm系統是唯一值，建議註冊時加上前後綴(例如: wm_abc123)。<br/>
        /// maxwin、maxlose、limitType为風險控管的一環。設定會員最大輸贏金額。請經營者多善利用。
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<MemberRegisterResponse> MemberRegisterAsync(Uri apiUrl, MemberRegisterRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 開遊戲 (獲取該會員的sid與網址，遊戲憑此連結跳轉至大廳。)
        /// </summary>
        /// <param name="apiUrl">The API URL.</param>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<SigninGameResponse> SigninGameAsync(Uri apiUrl, SigninGameRequest request, CancellationToken cancellationToken = default);
    }
}
