using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OnlineLearningAppApi.Hubs
{
    [Authorize]
    public class TeamHub : Hub
    {
        public async Task Send()
        {
            await Clients.All.SendAsync("Send", "hello");
        }
    }
}
