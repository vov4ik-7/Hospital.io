using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Psycho.DAL.Core;
using Psycho.DAL.Core.Domain;
using Psycho.DTO.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Psycho.io.Controllers
{
    [Authorize(Roles = "Psychologist,AuthorizedUser,AnonymousUser")]
    public class ChatController : Controller
    {
        private UserManager<User> _userManager;
        private IUnitOfWork _unitOfWork;

        public ChatController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public IActionResult ChatPage()
        {
            List<UserChatDTO> chats = new List<UserChatDTO>();

            var user = GetCurrentUserAsync().Result;

            var listChats = _unitOfWork.Context.Chats.Where(x => x.ReceiverId == user.Id || x.SenderId == user.Id).ToList();

            var listUniq = new List<int>();
            foreach (var elem in listChats)
            {
                if (elem.SenderId != user.Id && !listUniq.Contains(elem.SenderId))
                {
                    listUniq.Add(elem.SenderId);
                }
                else if (elem.ReceiverId != user.Id && !listUniq.Contains(elem.ReceiverId))
                {
                    listUniq.Add(elem.ReceiverId);
                }
            }

            foreach (var elem in listUniq)
            {
                List<Chat> chat = new List<Chat>();

                foreach (var elem2 in listChats)
                {
                    if (elem2.ReceiverId == elem || elem2.SenderId == elem)
                    {
                        chat.Add(elem2);
                    }
                }
                chat.Reverse();

                User userSender = _unitOfWork.Users.Get(elem);
                chats.Add(new UserChatDTO
                {
                    chat = chat,
                    User = userSender
                });
            }

            return View(chats);
        }

        [HttpGet]
        public async Task<int> GetCurrentUserId()
        {
            User usr = await GetCurrentUserAsync();
            return usr.Id;
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
