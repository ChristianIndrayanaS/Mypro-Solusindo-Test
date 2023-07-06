using Digimatika.Core.Models;
using Digimatika.Services.Interface.Interface;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace Digimatika.Controllers
{
    public class UserController : Controller
    {
        IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allUsers = await _userServices.GetAllUser();
            return View(allUsers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                var addUser = await _userServices.AddUser(user);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var getUser = await _userServices.GetUser(id);
            return View(getUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User user, string id)
        {
            await _userServices.UpdateUser(user,id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var getUser = await _userServices.GetUser(id);
            return View(getUser);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user, string id)
        {
            var getUser = await _userServices.GetUser(id);
            if(getUser != null)
            {
                await _userServices.DeleteUser(user, id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(user);
            }
        }
    }
}
