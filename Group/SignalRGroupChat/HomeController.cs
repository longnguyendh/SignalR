using Microsoft.AspNetCore.Mvc;

namespace SignalRGroupChat;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
