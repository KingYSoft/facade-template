using Abp.RealTime;
using FacadeCompanyName.FacadeProjectName.Web.Core.Hubs;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace FacadeCompanyName.FacadeProjectName.Web.Host.Hubs
{
    public class FacadeProjectNameHub : FacadeProjectNameHubCore
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
