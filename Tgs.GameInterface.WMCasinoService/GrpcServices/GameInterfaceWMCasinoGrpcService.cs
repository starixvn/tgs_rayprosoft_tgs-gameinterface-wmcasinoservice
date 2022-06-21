using Tgs.GameInterface.WMCasinoService.Core.ApplicationServices;
using Tgs.GameInterface.WMCasinoService.Protos;
using Tgs.ServiceUtility;

namespace Tgs.GameInterface.WMCasinoService.GrpcServices
{
    public class GameInterfaceWMCasinoGrpcService : GameInterfaceWMCasinoService.GameInterfaceWMCasinoServiceBase
    {
        private readonly IGetService<FundInQuery, FundInResult> _fundInService;
        private readonly IGetService<FundOutQuery, FundOutResult> _fundOutService;
        private readonly IGetService<BalanceQuery, BalanceResult> _balanceService;

        public GameInterfaceWMCasinoGrpcService(
            IGetService<FundInQuery, FundInResult> fundInService,
            IGetService<FundOutQuery, FundOutResult> fundOutService,
            IGetService<BalanceQuery, BalanceResult> balanceService)
        {
            _fundInService = fundInService ?? throw new ArgumentNullException(nameof(fundInService));
            _fundOutService = fundOutService ?? throw new ArgumentNullException(nameof(fundOutService));
            _balanceService = balanceService ?? throw new ArgumentNullException(nameof(balanceService));
        }
    }
}
