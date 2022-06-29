using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Task2.Models;

namespace Task2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment hostEnvironment;
        IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IHostingEnvironment _hostingEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            hostEnvironment = _hostingEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult SignUp()
        //{
        //    return View();
        //}
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            try
            {
                var Identity = (ClaimsIdentity)User.Identity;
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                
                    return RedirectToAction("SignIn");
            }
            catch
            {
                return RedirectToAction("SignIn");
            }
        }
        [Authorize]
        public IActionResult ThankYou()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        public IActionResult EomployeeRegistration()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> SubmitFormSignIn(SignIn signin)
        {
            var data = (dynamic)null;
            var prm = new
            {
                UserId = signin.UserId,
                Password = signin.Password
            };
            try
            {
                data = await dataLayer.QueryFirstOrDefaultAsyncWithDBResponse("SignIn", prm);
                if (Convert.ToInt32(data.responseCode) == 200)
                {
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, Convert.ToString(data.Id)));
                    claims.Add(new Claim(ClaimTypes.MobilePhone, Convert.ToString(data.MobileNumber)));
                    claims.Add(new Claim(ClaimTypes.GivenName, Convert.ToString(data.Name)));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Convert.ToString(data.Email)));
                    ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                    await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                }
            }
            catch (Exception ex)
            {
                data = ResponseResult.ExceptionResponse("internal server error", ex.Message.ToString());
            }
            return Ok(data);
        }
       

        [HttpPost]
        public async Task<IActionResult> SubmitFormDetails([FromBody] Details details)
        {
            var data = (dynamic)null;
            try
            {
                 var prm = new
                {
                    Name = details.Name,
                     Email = details.Email,
                    Gender = details.Gender,
                     MobileNumber = details.MobileNumber,
                     Address = details.Address,
                     PinCode = details.PinCode,
                     Password = details.Password,                  
                   
                };
                data = await dataLayer.QueryFirstOrDefaultAsyncWithDBResponse("InsertEmployeeDetail", prm);
            }
            catch (Exception ex)
            {
                data = ResponseResult.ExceptionResponse("Internal server Error", ex.Message.ToString());
            }
            return Ok(data);
        }
       
        
    }

    public class SignIn { 
    public string UserId { get; set; }
    public string Password { get; set; }
    }
    public class SignUp
    {
        public string UserId { get; set; }
        public string Password { get; set; }
    }

   
    public class Details
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }

        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }

    }
}
