using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace OnlineLearningAppApi.Infrastructure.Hubs
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
