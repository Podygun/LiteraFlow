﻿using System.Diagnostics;

namespace LiteraFlow.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrentUser currentUser;


        public HomeController(ILogger<HomeController> logger, ICurrentUser currentUser)
        {
            _logger = logger;
            this.currentUser = currentUser;
        }

        public async Task<IActionResult> Index()
        {
            return View(await currentUser.IsAuthorized());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}