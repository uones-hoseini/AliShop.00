using AliShop.Application.Visitors.VisitorOnLine;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace WebSite.EndPoint.Hubs
{
    public class OnLineVisitorHub:Hub
    {
        private readonly IVisitorOnLineService visitorOnLineService;
        public OnLineVisitorHub(IVisitorOnLineService visitorOnLineService)
        {
            this.visitorOnLineService = visitorOnLineService;
        }
 
        public override Task OnConnectedAsync()
        {
            visitorOnLineService.ConnectUser(Context.ConnectionId);
            var count=visitorOnLineService.GetCount();
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            visitorOnLineService.DisConnectUser(Context.ConnectionId);
            var count= visitorOnLineService.GetCount();  
            return base.OnDisconnectedAsync(exception);
        }
    }
}
