using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Web.Helpers;

namespace PvPGamingWebsite.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message, string color)
        {
            Clients.All.broadcastMessage(name, message, color);
        }

        public void OnJoin(string name, string color, string picture)
        {
            GlobalChatProperties.OnlineUsers.Add(new ChatUser { UserName = name, Color = color, Picture = picture });
            Clients.All.updateOnlineUsers(Json.Encode(GlobalChatProperties.OnlineUsers));
        }

        public void OnLeave(string name)
        {
            GlobalChatProperties.OnlineUsers.Remove(GlobalChatProperties.OnlineUsers.FirstOrDefault(x => x.UserName == name));
            Clients.All.updateOnlineUsers(Json.Encode(GlobalChatProperties.OnlineUsers));
        }
    }

    public static class GlobalChatProperties
    {
        public static List<ChatUser> OnlineUsers = new List<ChatUser>();
    }

    public class ChatUser
    {
        public string UserName { get; set; }
        public string Color { get; set; }
        public string Picture { get; set; }
    }
}