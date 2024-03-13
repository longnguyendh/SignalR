using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SignalRSample.Data;
using SignalRSample.Hubs;
using SignalRSample.Models;
using SignalRSample.Models.ViewModel;
using System.Diagnostics;
using System.Security.Claims;

namespace SignalRSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<ViewerCounterHub> _viewerCounterHub;
        private readonly IHubContext<OrderHub> _orderHub;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger,
            IHubContext<ViewerCounterHub> deathlyHub,
            IHubContext<OrderHub> orderHub,
            ApplicationDbContext context)
        {
            _logger = logger;
            _viewerCounterHub = deathlyHub;   
            _context = context;
            _orderHub = orderHub;
        }

        public IActionResult Index()
        {
            return View();
        
        }
        [Authorize]
        public IActionResult Chat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVm = new()
            {
                Rooms = _context.ChatRoom.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm);
        }
        public IActionResult RoomChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVm = new()
            {
                Rooms = _context.ChatRoom.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm);
        }
        public IActionResult BasicChat()
        {
            return View();
        }
        public async Task<IActionResult> Course(string type)
        {
            if (ViewerCounterDictionaryInMem.ViewerCounterDictionary.ContainsKey(type))
            {
                ViewerCounterDictionaryInMem.ViewerCounterDictionary[type]++;
            }
            await _viewerCounterHub.Clients.All.SendAsync("updateCourseCount",
                ViewerCounterDictionaryInMem.ViewerCounterDictionary[ViewerCounterDictionaryInMem.ReactjsCource],
                ViewerCounterDictionaryInMem.ViewerCounterDictionary[ViewerCounterDictionaryInMem.JavaCourse],
                ViewerCounterDictionaryInMem.ViewerCounterDictionary[ViewerCounterDictionaryInMem.CsharpCourse]);

            return Accepted();
        }

        public IActionResult Notification()
        {
            return View();
        }
        public IActionResult ViewerCounter()
        {
            return View();
        }
        public IActionResult HarryPotterHouse()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [ActionName("Order")]
        public async Task<IActionResult> Order()
        {
            string[] name = { "dev1", "dev2", "dev3", "dev4", "dev5" };
            string[] itemName = { "item1", "item2", "item3", "item4", "item5" };

            Random rand = new Random();
            // Generate a random index less than the size of the array.  
            int index = rand.Next(name.Length);

            Order order = new Order()
            {
                Name = name[index],
                ItemName = itemName[index],
                Count = index
            };

            return View(order);
        }

        [ActionName("Order")]
        [HttpPost]
        public async Task<IActionResult> OrderPost(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();
            await _orderHub.Clients.All.SendAsync("newOrder");
            return RedirectToAction(nameof(Order));
        }
        [ActionName("OrderList")]
        public async Task<IActionResult> OrderList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var productList = _context.Orders.ToList();
            return Json(new { data = productList });
        }

        public IActionResult GroupChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ChatVM chatVm = new()
            {
                Rooms = _context.ChatRoom.ToList(),
                MaxRoomAllowed = 4,
                UserId = userId,
            };
            return View(chatVm);
        }

    }
}