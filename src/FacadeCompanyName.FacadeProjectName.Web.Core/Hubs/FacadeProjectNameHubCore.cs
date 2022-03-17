using Abp.AspNetCore.SignalR.Hubs;
using Abp.RealTime;
using FacadeCompanyName.FacadeProjectName.DomainService.Share;

namespace FacadeCompanyName.FacadeProjectName.Web.Core.Hubs
{
    public class FacadeProjectNameHubCore : AbpCommonHub
    {
        private readonly IOnlineClientManager _onlineClientManager;
        public FacadeProjectNameHubCore(IOnlineClientManager onlineClientManager, IOnlineClientInfoProvider onlineClientInfoProvider)
            : base(onlineClientManager, onlineClientInfoProvider)
        {
            LocalizationSourceName = FacadeProjectNameConsts.ConnectionStringName;
            _onlineClientManager = onlineClientManager;
        }
    }
}