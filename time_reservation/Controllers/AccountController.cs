using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Plugins;
using System.Globalization;
using System.Security.Claims;
using time_reservation.Models;
using time_reservation.Repositories;

namespace time_reservation.Controllers
{
    public class AccountController : Controller
    {

        private IMemberRepository _memberRepository;

      
        public AccountController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }

            if (_memberRepository.IsExistUserByEmail(register.Email.ToLower()))
            {
                ModelState.AddModelError("Email", "ایمیل وارد شده قبلا ثبت نام کرده است");
                return View(register);
            }

            Member user = new Member()
            {
                FullName = register.FullName,
                Email = register.Email.ToLower(),
                Phone=register.Phone,
                Password = register.Password,
              
                DateCreated = DateTime.Now
            };

            _memberRepository.AddUser(user);

            return View("SuccessRegister", register);
        }

        public IActionResult VerifyEmail(string email)
        {
            if (_memberRepository.IsExistUserByEmail(email.ToLower()))
            {
                return Json($"ایمیل {email} تکراری است");
            }

            return Json(true);
        }
        #endregion

        #region Login

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

            var user = _memberRepository.GetUserForLogin(login.Email.ToLower(), login.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "اطلاعات صحیح نیست");
                return View(login);
            }

           var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.id.ToString()),
                new Claim(ClaimTypes.Name, user.FullName),
                
               


            };
          var   identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

           var  principal = new ClaimsPrincipal(identity);

          var   properties = new AuthenticationProperties
            {
                IsPersistent = login.RememberMe
            };

   
            HttpContext.SignInAsync(principal, properties);

            return Redirect("/");
        }


        #endregion

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }




        [HttpGet]
        public async Task<IActionResult> EditMember(int id)
        {
            var member = await _memberRepository.GetUserByIdAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            var model = new MemberProfileViewModel
            {
                Member = member,
                ChangePasswordViewModel = new ChangePasswordViewModel
                {
                    id = member.id 
                }
            };

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> EditMember(MemberProfileViewModel model)
        {
            var member = await _memberRepository.GetUserByIdAsync(model.Member.id);
            if (member == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                
                member.FullName = string.IsNullOrEmpty(model.Member.FullName) ? member.FullName : model.Member.FullName;
                member.Email = string.IsNullOrEmpty(model.Member.Email) ? member.Email : model.Member.Email;
                member.Phone = string.IsNullOrEmpty(model.Member.Phone) ? member.Phone : model.Member.Phone;
             
               

                await _memberRepository.UpdateMemberAsync(member);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }



        [HttpPost]
        public async Task<IActionResult> ChangePassword(MemberProfileViewModel model)
        {
          
            var member = await _memberRepository.GetUserByIdAsync(model.ChangePasswordViewModel.id);
            if (member == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {

                if(member.Password == model.ChangePasswordViewModel.CurrentPassword && !String.IsNullOrEmpty(model.ChangePasswordViewModel.CurrentPassword))
                {
                   
                   
                   if(!String.IsNullOrEmpty(model.ChangePasswordViewModel.NewPassword)&& !String.IsNullOrEmpty(model.ChangePasswordViewModel.ConfirmNewPassword) && (model.ChangePasswordViewModel.NewPassword == model.ChangePasswordViewModel.ConfirmNewPassword))
                    {
                        member.Password = model.ChangePasswordViewModel.NewPassword;
                    }

                }

               

                await _memberRepository.UpdateMemberAsync(member);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }





        [HttpPost]
        public async Task<IActionResult> AddTTimeByMember(AddTTimeForMember model)
        {

            if (model == null)
            {
                return RedirectToAction("Admin", "Index");
            }

         



            var business = _memberRepository.GetBusinessById(model.BusinessId);

            var member = await _memberRepository.GetUserByIdAsync(model.MemberId);








            TTime ttime = new TTime()
            {
                AdminIdT = business.AdminIdB,
                MemberId = member.id,
                BusinessId = business.id,
                FullName = model.UserName,
                BusinessName =business.Name ,
                PictureFileName = business.PictureFileName,
                BusinessAddress = business.City + " / "+business.SubCity+" / "+business.Address,
                ReservationHour = model.ReservationHour,
                ReservationDate = model.ReservationDate,
                IsPaymentCompleted =true,
                DateCreated = DateTime.Now
            };

            _memberRepository.AddTTime(ttime);

            return RedirectToAction("Index", "Home");
        }




       











    }
}
