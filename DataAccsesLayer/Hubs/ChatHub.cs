using DataAccsesLayer.Models;
using Microsoft.AspNetCore.SignalR;

namespace DataAccsesLayer.Hubs;

public class ChatHub : Hub
{
    public async Task JoinToChat(User user)
    {
        await Clients.Others.SendAsync("JoinToChat", $"{user.FullName} ", DateTime.UtcNow.AddHours(5));
    }

    public async Task LeftFromChat(User user)
    {
        await Clients.Others.SendAsync($"{user.FullName} join left group {DateTime.UtcNow.AddHours(5)}");
    }

    public async Task SendMessage(User user, string message)
    {
        await Clients.All.SendAsync("message", $"{user.FullName} {DateTime.UtcNow.AddHours(5)}");
    }
}
