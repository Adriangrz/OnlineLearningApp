using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hubs
{
    [Authorize]
    public class QuizHub : Hub
    {
        public async Task Send()
        {
            await Clients.All.SendAsync("Send", "hello");
        }
    }
}
