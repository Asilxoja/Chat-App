using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DataAccsesLayer.Hubs
{
    public class ChatHub : Hub
    {
        private static Dictionary<string, string> connectedClients = new Dictionary<string, string>();
        public async Task SendMessage(string user, string message)
        {
            var timestamp = DateTime.UtcNow;
            var year = DateTime.UtcNow.AddHours(5).Year;
            var month = DateTime.UtcNow.AddHours(5).Month;
            var hour = DateTime.UtcNow.AddHours(5).Hour;
            var minute = DateTime.UtcNow.AddHours(5).Minute;
            var day = DateTime.UtcNow.AddHours(5).Day;
            var result = $"{year}:{month}:{day}:{hour}:{minute}";   
            await Clients.All.SendAsync("ReceiveMessage", user, message, result);
        }

        private string Time(DateTime adjustedTime)
        {
            if (adjustedTime.Minute == DateTime.UtcNow.AddHours(5).Minute)
            {
                return "now";
            }
            return adjustedTime.Minute.ToString();
        }

        public async Task JoinChat(string user, string message)
        {
            connectedClients[Context.ConnectionId] = user;
            await Clients.Others.SendAsync("ReceiveMessage", user, message);
        }

        private async Task LeaveChat()
        {
            if (connectedClients.TryGetValue(Context.ConnectionId, out string? user))
            {
                var timestamp = DateTime.UtcNow;
                var timeAgo = CalculateTimeAgo(timestamp);
                var leaveMessage = $"{user} left the chat";
                await Clients.Others.SendAsync("ReceiveMessage", user, leaveMessage, timeAgo);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await LeaveChat();
            await base.OnDisconnectedAsync(exception);
        }

        private string CalculateTimeAgo(DateTime timestamp)
        {
            var timeElapsed = DateTime.UtcNow - timestamp;

            if (timeElapsed.TotalMinutes < 1)
            {
                return "just now";
            }
            else if (timeElapsed.TotalSeconds < 2)
            {
                return $"{(int)timeElapsed.TotalMinutes} sec ago";
            }
            else if (timeElapsed.TotalHours < 24)
            {
                return $"{(int)timeElapsed.TotalHours} hours ago";
            }
            else
            {
                return $"{(int)timeElapsed.TotalDays} days ago";
            }
        }
    }
}
