using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net.Mail;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Customer()
        {
            Cls_Customer _obj = new Cls_Customer();
            _obj._CustList = new List<Cls_Customer>();
            DataTable dt = _obj.Select_Customer(HttpContext.Session.GetString("SysAccountUUid"));
            _obj._CustList = BindCustomerList(dt);
            return View(_obj);
        }

        [HttpPost]

        public ActionResult Customer(Cls_Customer _objCM, string Save, string Update)
        {
            if (ModelState.IsValid)
            {
                _objCM.Postedby = HttpContext.Session.GetString("SysAccountUUid");
                if (!string.IsNullOrEmpty(Save))
                {
                    //_objCM.lineid = 0;
                    DataTable dtSave = _objCM.Insert_Customer(_objCM);
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

                DataTable dt = _objCM.Select_Customer(HttpContext.Session.GetString("SysAccountUUid"));
                _objCM._CustList = BindCustomerList(dt);

            }
            return View(_objCM);
        }

        public List<Cls_Customer> BindCustomerList(DataTable dt)
        {
            int i = 0;
            List<Cls_Customer> _locationlist = new List<Cls_Customer>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    _locationlist.Add(new Cls_Customer
                    {

                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        CustUUid = dr["CustomerUUid"].ToString(),
                        CustomerName = dr["CustomerName"].ToString().ToUpper(),
                        CustomerEmail = dr["Email"].ToString().ToUpper(),
                        CustomerPhone = dr["phone"].ToString(),
                        Status = dr["Status"].ToString().ToUpper(),
                        Postedby = dr["Postedby"].ToString().ToUpper(),


                    });
                }
            }
            return _locationlist;
        }

        public IActionResult AddCustomer()
        {
            Cls_Customer obj = new Cls_Customer();
            return PartialView(obj);
        }

        public IActionResult Email()
        {
            return View();
        }

    }
}
