using Visitor_Management.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Mail;
using System.Security.Principal;

namespace Visitor_Management.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login()
        {
            Cls_Login person = new Cls_Login();
            person.Message = "";
            return View(person);
        }

        [HttpPost]
        public ActionResult Login(Cls_Login person)
        {
            try
            {
                Cls_Login _Login = new Cls_Login();
                var validate = _Login.ValidateUserAccount(person.UserName, person.Password);

                if (validate.Rows.Count > 0)
                {

                    HttpContext.Session.SetString("LocationName", validate.Rows[0]["LocationName"].ToString());
                    HttpContext.Session.SetString("accounttype", validate.Rows[0]["accounttype"].ToString());
                    HttpContext.Session.SetString("FullName", validate.Rows[0]["FullName"].ToString());
                    HttpContext.Session.SetString("SysAccountUUid", validate.Rows[0]["SysAccountUUid"].ToString());
                    HttpContext.Session.SetString("BaseLocation", validate.Rows[0]["BaseLocation"].ToString());
                    HttpContext.Session.SetString("Email", validate.Rows[0]["Email"].ToString());

                    return RedirectToAction("MasterVisitor", "Visitor");
                }
                else
                {
                    ViewBag.Message = "Invalid username or password!! Please try again.";
                    return View(person);
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "An Error Occurred!!";
                return View(person);
            }
        }

        public ActionResult Logout()
        {
            Cls_Login _obj = new Cls_Login();
            return View("login", _obj);
        }

        public ActionResult ForgetPassword()
        {
            Cls_Login _obj = new Cls_Login();
            return View("ForgetPassword", _obj);
        }

        [HttpPost]
        public ActionResult ForgetPassword(Cls_Login obj, string UserName, string Save)
        {
            Cls_Login _obj = new Cls_Login();

            if (ModelState.IsValid)
            {

                if (!string.IsNullOrEmpty(Save))
                {
                    var validate = _obj.FogetPassword(obj);

                    if (validate.Rows.Count > 0)
                    {

                        try
                        {
                            //string htmlContent = this.RenderViewToString("DesignPage.cshtml");
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                            mail.From = new MailAddress("bohraaaryan2002@gmail.com");
                            mail.To.Add(UserName);
                            mail.Subject = "Account Recovery";

                            mail.IsBodyHtml = true;
                            string htmlBody;

                            htmlBody = "Your Account Password is " + validate.Rows[0]["Password"].ToString();

                            mail.Body = htmlBody;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential("bohraaaryan2002@gmail.com", "Aryan12_@");
                            SmtpServer.EnableSsl = true;

                            SmtpServer.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                    }

                }
                return View("Login", _obj);
            }

            return View("ForgetPassword", _obj);


        }

        public IActionResult ChangePassword()
        {
            Cls_Login _obj = new Cls_Login();
            return View(_obj);
        }

        [HttpPost]
        public IActionResult ChangePassword(Cls_Login obj, string ChangePassword)
        {
            List<Cls_Login> _uploadList = new List<Cls_Login>();
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(ChangePassword))
                {

                    DataTable dtSave = obj.ChangePassword(obj);
                    if (dtSave != null && dtSave.Rows.Count > 0)
                    {

                        ViewBag.Message = dtSave.Rows[0][0].ToString();
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "An error occurred.";
                        ModelState.Clear();
                    }

                }

                return View("ChangePassword", _uploadList);
            }
            
            return View("Login",_uploadList);
        }

    }
}
