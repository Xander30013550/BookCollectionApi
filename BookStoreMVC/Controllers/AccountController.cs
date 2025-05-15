using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using BookStoreMVC.Models;

namespace BookStoreMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        public AccountController(IHttpClientFactory factory)
        {
            _client = factory.CreateClient("ApiClient");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            var response = await _client.PostAsJsonAsync("api/user/register", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Login");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(UserViewModel model)
        {
            var response = await _client.PostAsJsonAsync("api/user/login", model);
            if (response.IsSuccessStatusCode)
                return RedirectToAction("Welcome");

            ViewBag.Error = await response.Content.ReadAsStringAsync();
            return View(model);
        }

        public IActionResult Welcome() => View();
    }
}
