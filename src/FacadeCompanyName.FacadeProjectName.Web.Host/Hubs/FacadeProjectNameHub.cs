using Abp.AspNetCore.SignalR.Hubs;
using Abp.RealTime;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Hubs
{
    public class FacadeProjectNameHub : AbpCommonHub
    {
        public FacadeProjectNameHub(IOnlineClientManager onlineClientManager, IOnlineClientInfoProvider onlineClientInfoProvider)
            : base(onlineClientManager, onlineClientInfoProvider)
        {
        }
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
