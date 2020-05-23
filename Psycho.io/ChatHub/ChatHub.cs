using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Psycho.io.ChatForUser
{
    public interface IChatHub
    {
        Task LiveChatMessageReceived(int fromId, string fromEmail, string message);
    }

    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        public async Task SendLiveChatMessage(string to, string message)
        {
            await Clients.User(to).LiveChatMessageReceived(1, Context.UserIdentifier, message);
        }
    }
}
