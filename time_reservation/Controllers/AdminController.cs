using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using time_reservation.Models;
using time_reservation.Repositories;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;




namespace time_reservation.Controllers
{
    public class AdminController : Controller
    {

        private IAdminRepository _adminRepository;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminController(IAdminRepository adminRepository, IWebHostEnvironment hostingEnvironment)
        {
            _adminRepository = adminRepository;
            _hostingEnvironment = hostingEnvironment;
        }



        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> AdminProfileAsync(int id)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(id);

         


            if (admin == null)
            {
                return NotFound();
            }

            var model = new AdminProfileViewModel
            {
                Admin = admin,
               
               
                ChangePasswordViewModel = new ChangePasswordViewModel
                {
                    id = admin.id 
                }
                
            };

            return View(model);
        }


       

        #region Admin manager


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var admin = _adminRepository.GetAdminForLogin(login.Email.ToLower(), login.Password);
            if (admin == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(login);
            }



            return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterAdminViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_adminRepository.IsExistUserByEmail(register.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام کرده است");
                return View(register);
            }

            Admin admin = new Admin()
            {
                FullName = register.FullName,
                NationalCode = register.NationalCode,
                Email = register.Email.ToLower(),
                Phone = register.Phone,
                Password = register.Password,
                State = true,

                DateCreated = DateTime.Now
            };

            _adminRepository.AddAdmin(admin);


            string wwwrootPath = _hostingEnvironment.WebRootPath+"/images/admins";
         
            string newFolderPath = Path.Combine(wwwrootPath, admin.id.ToString());

        
            if (!Directory.Exists(newFolderPath))
            {
                Directory.CreateDirectory(newFolderPath);
            }



            return View("SuccessRegister", register);
        }

        public IActionResult VerifyEmail(string email)
        {
            if (_adminRepository.IsExistUserByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }

            return Json(true);
        }
   



        [HttpPost]
    
        public async Task<IActionResult> EditAdmin(AdminProfileViewModel model)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(model.Admin.id);
            if (admin == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
             
                admin.FullName = string.IsNullOrEmpty(model.Admin.FullName) ? admin.FullName : model.Admin.FullName;
                admin.NationalCode = string.IsNullOrEmpty(model.Admin.NationalCode) ? admin.NationalCode : model.Admin.NationalCode;
                admin.Email = string.IsNullOrEmpty(model.Admin.Email) ? admin.Email : model.Admin.Email;
                admin.Phone = string.IsNullOrEmpty(model.Admin.Phone) ? admin.Phone : model.Admin.Phone;
               

                await _adminRepository.UpdateAdminAsync(admin);
                return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
            }

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> ChangePassword(AdminProfileViewModel model)
        {
         
            var admin = await _adminRepository.GetAdminByIdAsync(model.ChangePasswordViewModel.id);
            if (admin == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {

                if (admin.Password == model.ChangePasswordViewModel.CurrentPassword && !String.IsNullOrEmpty(model.ChangePasswordViewModel.CurrentPassword))
                {


                    if (!String.IsNullOrEmpty(model.ChangePasswordViewModel.NewPassword) && !String.IsNullOrEmpty(model.ChangePasswordViewModel.ConfirmNewPassword) && (model.ChangePasswordViewModel.NewPassword == model.ChangePasswordViewModel.ConfirmNewPassword))
                    {
                        admin.Password = model.ChangePasswordViewModel.NewPassword;
                    }
                   

                }
                



                await _adminRepository.UpdateAdminAsync(admin);
                return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
            }

            return View(model);
        }




        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }

        #endregion



        #region TTime manager

        public async Task<IActionResult> AddReserve(int id)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            var addttime = new AddTTimeViewModel
            {
                Admin = admin,
            };



            return View(addttime);
        }

        [HttpPost]
        public async Task<IActionResult> AddReserveAsync(AddTTimeViewModel model)
        {

            var admin = await _adminRepository.GetAdminByIdAsync(model.id);

            if (admin == null)
            {
                return View(model);
            }

            Member member = new Member
            {
                FullName = model.FullName,
                DateCreated = DateTime.Now
                
            };

            _adminRepository.AddMember(member);

          

            var business = _adminRepository.GetBusinessById(model.SelectBusiness);

            TTime ttime = new TTime()
            {
                AdminIdT = admin.id,

                BusinessId = business.id,
                MemberId = member.id,



                FullName = model.FullName,

                BusinessName = business.Name,

                PictureFileName = business.PictureFileName,
               
                BusinessAddress = business.City + " / "+business.SubCity+" / " + business.Address,

                ReservationHour = model.ReservationHour,
             
                ReservationDate = model.ReservationDate, 
                IsPaymentCompleted = true, 
                DateCreated = DateTime.Now 

            };

            _adminRepository.AddTTime(ttime);

            return RedirectToAction("AdminProfile", "Admin", new { id = model.id });


        }










        [HttpGet]
        public async Task<IActionResult> EditReserve(int id)
        {
            var reservation =  _adminRepository.GetTTimeById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            var admin = await _adminRepository.GetAdminByIdAsync(reservation.AdminIdT);
            if (admin == null)
            {
                return NotFound();
            }

            var viewModel = new AddTTimeViewModel
            {
                TTime = reservation,
                SelectBusiness = reservation.BusinessId,
                Admin = admin
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditReserve(AddTTimeViewModel model)
        {
            var business = _adminRepository.GetBusinessById(model.SelectBusiness);
            if (!ModelState.IsValid)
            {
                var tTime =  _adminRepository.GetTTimeById(model.TTime.id);
                var admin = await _adminRepository.GetAdminByIdAsync(tTime.AdminIdT);
                if (tTime == null)
                {
                    return NotFound();
                }

                tTime.FullName = model.TTime.FullName;
                tTime.BusinessName = business.Name;
                tTime.PictureFileName = business.PictureFileName;
                tTime.BusinessId = model.SelectBusiness;
                tTime.ReservationHour = model.TTime.ReservationHour;
                tTime.ReservationDate = model.TTime.ReservationDate;
                tTime.IsPaymentCompleted = model.TTime.IsPaymentCompleted;

                await _adminRepository.UpdateTTimeAsync(tTime);
                return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
            }

            return View(model);
        }






        public IActionResult DeleteReserve(int id)
        {

            var ttime = _adminRepository.GetTTimeById(id);

            var admintime = ttime.AdminIdT;

            if (ttime == null)
            {
                return View();
            }

            _adminRepository.DeleteTTime(ttime);


            return RedirectToAction("AdminProfile", "Admin", new {id = admintime});

        }


        #endregion




        #region Business Manager

        public async Task<IActionResult> CreateBusiness(int id) 
        {

            var admin = await _adminRepository.GetAdminByIdAsync(id);

            var add_business_vm = new AddBusinessViewModel
            {
                Admin = admin,
            };



            return View(add_business_vm);
        }


        [HttpPost]
        public async Task<IActionResult> CreateBusiness(AddBusinessViewModel model)
        {
            var admin = await _adminRepository.GetAdminByIdAsync(model.id);
            if (model == null)
            {
                return NotFound();
            }

            var b = new Business()
            {
                Name = model.Name,
                AdminIdB = admin.id,
                OwnerName = admin.FullName,
                Description = model.Description,
                Possibilities = model.Possibilities,
                Gender = model.Gender,
                City = model.City,
                SubCity = model.SubCity,
                Address = model.Address,
                TimeUnit = model.TimeUnit,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                Price = model.Price,
                DateCreated = DateTime.Now,
                State = model.State
            };

            if (model.Picture?.Length > 0)
            {
            
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "admins", admin.id.ToString(), model.Name);

         
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                string fileExtension = Path.GetExtension(model.Picture.FileName);

           
                string fileName = $"{admin.id}{model.Name}{fileExtension}";
                string filePath = Path.Combine(directoryPath, fileName);

                using (var image = Image.Load(model.Picture.OpenReadStream()))
                {
                    var options = new JpegEncoder { Quality = 75 }; 
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(800, 800) 
                    }));
                    image.Save(filePath, options);
                }

                b.PictureFileName = fileName;
                _adminRepository.AddBusiness(b);
            }

            return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });

        }










        [HttpGet]
        public async Task<IActionResult> EditBusiness(int id)
        {
            var business =  _adminRepository.GetBusinessById(id);
            var admin = _adminRepository.GetAdminByBusinessId(id);
            if (business == null)
            {
                return NotFound();
            }





            var model = new EditBusinessViewModel
            {
                Admin = await admin,
                id = business.id,
                Name = business.Name,
                PictureFileName = business.PictureFileName,
                Description = business.Description,
                Possibilities = business.Possibilities,
                Gender = business.Gender,
                City = business.City,
                SubCity = business.SubCity,
                Address = business.Address,
                TimeUnit = business.TimeUnit,
                StartTime = business.StartTime,
                EndTime = business.EndTime,
                Price = business.Price,
                State = business.State
            };

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> EditBusiness(EditBusinessViewModel model)
        {
            var business =  _adminRepository.GetBusinessById(model.id);
            var newImageNames = new List<string>();
            if (business == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                return View(model);
            }

            var admin = await _adminRepository.GetAdminByIdAsync(business.AdminIdB);




            string fileName = "";
            if (model.Picture?.Length > 0)
            {
              
                string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "admins", admin.id.ToString(), model.Name);

                
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

          
                string fileExtension = Path.GetExtension(model.Picture.FileName);

              
                 fileName = $"{admin.id}{model.Name}{fileExtension}";
                string filePath = Path.Combine(directoryPath, fileName);

          
                using (var image = Image.Load(model.Picture.OpenReadStream()))
                {
                    var options = new JpegEncoder { Quality = 75 }; 
                    image.Mutate(x => x.Resize(new ResizeOptions
                    {
                        Mode = ResizeMode.Max,
                        Size = new Size(800, 800) 
                    }));
                    image.Save(filePath, options);
                }

                business.PictureFileName = fileName;
                
            }



















            business.Name = model.Name;
            business.Description = model.Description;
            business.Possibilities = model.Possibilities;
            business.Gender = model.Gender;
            business.City = model.City;
            business.SubCity = model.SubCity;
            business.Address = model.Address;
            business.TimeUnit = model.TimeUnit;
            business.StartTime = model.StartTime;
            business.EndTime = model.EndTime;
            business.Price = model.Price;
            business.State = model.State;
    

          

            await _adminRepository.UpdateBusinessAsync(business);


            var ttime = _adminRepository.GetTTimesByBusinessIdAsync(business.id);

            foreach (var item in await ttime)
            {
                item.BusinessName = model.Name;
                item.PictureFileName = fileName;
                item.BusinessAddress = model.City + " / " + model.SubCity + " / " + model.Address;
                await _adminRepository.UpdateTTimeAsync(item);
            }






            return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
        }









        public async Task<IActionResult> DeleteBusiness(int id)
        {



            var business =  _adminRepository.GetBusinessById(id);

            var admin = await _adminRepository.GetAdminByIdAsync(business.AdminIdB);

            if (business == null)
            {
                return NotFound();
            }

            if (business.TTime.Any())
            {
                var model = new AdminProfileViewModel
                {
                    Business = business,
                    Admin = admin,
                };
                ModelState.AddModelError(string.Empty, "شما نمی توانید سالنی که رزرو شده است را حذف کنید آن را غیر فعال کنید.");
                return View("AdminProfile", model);
            }

            _adminRepository.DeleteBusiness(business);

            return RedirectToAction("AdminProfile", "Admin", new { id = admin.id });
        }




        #endregion




    }
}
