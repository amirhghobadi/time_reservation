using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;
using time_reservation.Data;
using time_reservation.Helper;
using time_reservation.Models;
using time_reservation.Repositories;

namespace time_reservation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private TimeReservationContext _context;

        private IAdminRepository _adminRepository;

        public HomeController(ILogger<HomeController> logger,TimeReservationContext context,IAdminRepository adminRepository)
        {
            _logger = logger;
            _context = context;
            _adminRepository = adminRepository;

        }

        public IActionResult Index()
        {

         
            var businesses = _context.Businesses.ToList().Where(x => x.State ==true);

            if(businesses != null)
            {
                return View(businesses);
            }

            return View();
        }


        public async Task<IActionResult> Detail(int id)
        {
           

            var b = _adminRepository.GetBusinessById(id);


            if (b == null)
            {
                return NotFound();
            }



            var ttimes = b.TTime.ToList();

          
            ViewBag.TTimes = ttimes;

            Admin admin = await _adminRepository.GetAdminByIdAsync(b.AdminIdB);

            ViewBag.Admin = admin.Phone ;






            return View(b);
        }


        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
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
