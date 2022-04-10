using QueryRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Security;
using static QueryRoom.Controllers.AccountAPIController;

namespace QueryRoom.Controllers
{
    [AllowAnonymous]
    public class AccountMVCController : Controller
    {
        //Registration Through API
        public ActionResult Registration()                                             //OKK for dtos
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(accountClass obj)                             //OKK for dtos
        {
            if(ModelState.IsValid)
            {
                HttpClient hc = new HttpClient();

                //port number to be replaced as per the developer's localhost
                hc.BaseAddress = new Uri("https://localhost:44317/api/AccountAPI");


                var insertRec = hc.PostAsJsonAsync<accountClass>("AccountAPI", obj);
                insertRec.Wait();

                var saveRec = insertRec.Result;
                if (saveRec.IsSuccessStatusCode)
                {
                    Response.Write("<script>alert('The UserName : " + obj.USERNAME + " is registered')</script>");
                    return View("RegConfirmed");
                }
            }
            return View();
        }

        //Login
        public ActionResult Login()                                                                      //OKK for dtos
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "HomePage");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginData obj)                                                         //Changed and OKK for dtos
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44317/api/LoginAPI");
            var postData = hc.PostAsJsonAsync<LoginData>("LoginAPI", obj);
            postData.Wait();

            var res = postData.Result;
            if (res.IsSuccessStatusCode)
            {
                var ticket = new FormsAuthenticationTicket(obj.USERNAME, true, 525600);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(525600);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                var result = res.Content.ReadAsAsync<string>();
                result.Wait();
                var role = result.Result;
                if (role=="Admin")
                {
                    return RedirectToAction("Index", "AdminMVC");
                }
                return RedirectToAction("Index", "HomePage");
            }                
            else
            {
                ModelState.AddModelError("", "Invalid Username or Password ");
                return View();
            }            
        }


        //Logout

        public ActionResult Logout()                                                                    //OKK for dtos
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        //REg Confirmed
        
        public ActionResult RegConfirmed()                                                              //OKK for dtos
        {
            return View();
        }

    }
}