using Microsoft.AspNetCore.SignalR;

namespace DeploymentTracker.web.Hubs
{
    public class NotificationsHub : Hub
    {
        public void Send(string name, string message)
        {
            // updating all clients
            Clients.All.InvokeAsync("broadcastMsg", $"{name} said: {message}");
        }
    }
}