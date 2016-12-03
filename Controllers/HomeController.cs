using Microsoft.AspNetCore.Mvc;

namespace WebSocketsNetCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    } 
}