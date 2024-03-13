using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRSample.Data;

namespace SignalRSample.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _db;
        public BasicChatHub(ApplicationDbContext db)
        {
            _db = db;
        }

        [Authorize]
        public async Task SendMessageToAll(string user, string message)
        {
            if(!string.IsNullOrEmpty(message)) { 
                await Clients.All.SendAsync("MessageReceived", user, message);
            }
        }
        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var userId = (await _db.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == receiver.ToLower()))?.Id;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(message))
            {
                await Clients.User(userId).SendAsync("MessageReceived", sender, message);
            }

        }

    } 
}
