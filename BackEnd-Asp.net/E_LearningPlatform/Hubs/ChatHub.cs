using Microsoft.AspNetCore.SignalR;

namespace E_LearningPlatform.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(string name, string message)
        {
            Clients.All.SendAsync("newMsg", name, message);

        }
    }
}
