using DataAccsesLayer.Models;
using Microsoft.AspNetCore.SignalR;

namespace DataAccsesLayer.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage (string user,string message)
    {
        await Clients.All.SendAsync("RegiveMessage", user, message);
    }

    public async Task JoinChat(string user, string message)
    {
        await Clients.Others.SendAsync("RegiveMessage", user, message);
    }
}

