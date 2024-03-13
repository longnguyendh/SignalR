// Hubs/ChatHub.cs

using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    public async Task SendMessage(string user, string message, string groupName)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
    }

    public async Task JoinGroup(string user, string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{user} has joined the group {groupName}");
    }

    public async Task LeaveGroup(string user, string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{user} has left the group {groupName}");
    }

    //public async Task AddUserToGroup(string userId, string groupName)
    //{
    //    await Groups.AddToGroupAsync(userId, groupName);
    //    await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{userId} has been added to the group {groupName}");
    //}

    //public async Task RemoveUserFromGroup(string userId, string groupName)
    //{
    //    await Groups.RemoveFromGroupAsync(userId, groupName);
    //    await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{userId} has been removed from the group {groupName}");
    //}
}
