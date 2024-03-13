using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

public class GroupChatHub : Hub
{
    [Authorize]
    public async Task SendMessage(string user, string message, string groupName)
    {
        await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message, groupName);
    }

    public async Task JoinGroup(string user, string groupName)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{user} has joined the group {groupName}", groupName);
    }

    public async Task LeaveGroup(string user, string groupName)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        await Clients.Group(groupName).SendAsync("ReceiveMessage", "System", $"{user} has left the group {groupName}", groupName);
    }
}
