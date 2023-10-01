using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Net;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class ExistingVisitorController : Controller
    {
        public IActionResult ExistingVisitor()
        {
            Cls_Visitor _obj = new Cls_Visitor();
            _obj._ExistingVList = new List<Cls_Visitor>();
            DataTable ds = _obj.Select_Existing_Visitor(HttpContext.Session.GetString("BaseLocation"));
            _obj._ExistingVList = BindExistingVisitor(ds);

            return View(_obj);
        }
        public List<Cls_Visitor> BindExistingVisitor(DataTable dt)
        {
            List<Cls_Visitor> lst = new List<Cls_Visitor>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    var url = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/api/GetEmployeeImageBlob/" + dr["visitor_id"].ToString();

                    lst.Add(new Cls_Visitor()
                    {
                        //LineId = Convert.ToInt32(dr["LineID"].ToString()),
                        Visitor_Id = dr["Visitor_Id"].ToString(),
                        Name = dr["Name"].ToString(),
                        EntryTime = dr["EntryTime"].ToString(),
                        ExitTime = string.IsNullOrEmpty(dr["ExitTime"].ToString()) ? "N/A" : dr["ExitTime"].ToString().ToUpper(),
                        Totalhours = string.IsNullOrEmpty(dr["Totalhours"].ToString()) ? "N/A" : dr["Totalhours"].ToString().ToUpper(),
                        emailid = dr["emailid"].ToString(),
                        phone = dr["phone"].ToString(),
                        idproofnumber = dr["idproofnumber"].ToString(),
                        companyname = dr["companyname"].ToString(),
                        address = dr["address"].ToString(),
                        status = dr["status"].ToString(),
                        minors = dr["minors"].ToString(),
                        adults = dr["adults"].ToString(),
                        photo = url,
                        Present_Visitor = dr["present_visitor"].ToString(),
                        Exit_Visitor = dr["exit_visitor"].ToString(),
                        No_Of_Visitor = Convert.ToInt32(dr["total_visitor"].ToString()),
                        posteddatetime = dr["posteddatetime"].ToString(),
                        visitortype = dr["visitortype"].ToString(),
                        VisitorCardNo = dr["visitorcardno"].ToString(),
                        accessories = dr["accessories"].ToString(),
                        idproof = dr["idproof"].ToString(),
                        departmentname = dr["departmentname"].ToString(),
                        EmpName = dr["EmpName"].ToString(),
                        purpose_of_visit = dr["purpose_of_visit"].ToString(),
                        Postedby = dr["FullName"].ToString(),

                    });
                }
            }
            return lst;
        }
        [HttpPost]
        public IActionResult ExistingVisitor(Cls_Visitor _obj, Microsoft.AspNetCore.Http.IFormFile postedFile, IFormCollection form, string save, string Upload, string Update, string status, string Confirm)
        {

            List<Cls_Visitor> _uploadList = new List<Cls_Visitor>();
            if (ModelState.IsValid)
            {
                _obj.Postedby = HttpContext.Session.GetString("SysAccountUUid");
                _obj.BranchId = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(save))
                {

                    // _obj.SysVisitorCardId = '0';
                    DataTable dtSave = _obj.Insert_ExistimngVisitor(_obj);
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
               
                DataTable dt = _obj.Select_Existing_Visitor((HttpContext.Session.GetString("BaseLocation")));
                _obj._VList = BindExistingVisitor(dt);

            }
            return View(_obj);
        }
        public IActionResult AddExistingVisitor(string Visitor_id)
        {
            Cls_Visitor obj = new Cls_Visitor();
            DataTable dts = new DataTable();
            dts = obj.Select_VisitorDetails(Visitor_id);

            if (dts != null && dts.Rows.Count > 0)
            {
                foreach (DataRow dr in dts.Rows)
                {
                        obj.Visitor_Id = dr["Visitor_Id"].ToString();
                        obj.Name = dr["Name"].ToString();
                        obj.emailid = dr["emailid"].ToString();
                        obj.phone = dr["phone"].ToString();
                        obj.idproofnumber = dr["idproofnumber"].ToString();
                        obj.companyname = dr["companyname"].ToString();
                        obj.address = dr["address"].ToString();
                        obj.status = dr["status"].ToString();
                        obj.minors = dr["minors"].ToString();
                        obj.adults = dr["adults"].ToString();                    
                        obj.visitortype = dr["visitortype"].ToString();
                        obj.accessories = dr["accessories"].ToString();
                        obj.idproof = dr["idproof"].ToString();
                        obj.departmentname = dr["departmentname"].ToString();
                        obj.EmpName = dr["EmpName"].ToString();
                        obj.purpose_of_visit = dr["purpose_of_visit"].ToString();
                }
            }
            DataTable dt = obj.Get_VCardNo_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt1 = obj.Get_Accessories_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt2 = obj.Get_Department_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt3 = obj.Get_Employee_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt4 = obj.Get_Visitor_Type_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt5 = obj.Get_PurposeOf_Visit_DD((HttpContext.Session.GetString("BaseLocation")));
            DataTable dt6 = obj.Get_Idproof_DD((HttpContext.Session.GetString("BaseLocation")));
            obj.VCardNoDDL = BindData(dt);         
            obj.AccessoriesDDL = BindData(dt1);
            obj.DepartmentDDL = BindDepartment(dt2);
            obj.EmployeeDDL = BindData(dt3);
            obj.VisitorTypeDDL = BindData(dt4);
            obj.PurposeDDL = BindData(dt5);
            obj.IdProofDDL = BindData(dt6);
            obj.AdultDDL = VList();
            obj.MinorsDDL = VList();
            return PartialView("AddExistingVisitor",obj);
        }

        public List<SelectListItem> BindData(DataTable dt)
        {
            List<SelectListItem> NameList = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    NameList.Add(new SelectListItem
                    {
                        Text = dr["text"].ToString(),
                        Value = dr["value"].ToString(),
                        Selected = true

                    });
                }
            }
            return NameList;
        }
           
        public List<SelectListItem> BindDepartment(DataTable dt)
        {
            List<SelectListItem> NameList = new List<SelectListItem>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    NameList.Add(new SelectListItem
                    {
                        Text = dr["text"].ToString(),
                        Value = dr["text"].ToString(),
                        Selected = true

                    });
                }
            }
            return NameList;
        }
     

        public List<SelectListItem> VList()
        {
            List<SelectListItem> _obj = new List<SelectListItem>();
            _obj.Add(new SelectListItem { Text = "1", Value = "1" });
            _obj.Add(new SelectListItem { Text = "2", Value = "2" });
            _obj.Add(new SelectListItem { Text = "3", Value = "3" });
            _obj.Add(new SelectListItem { Text = "4", Value = "4" });
            _obj.Add(new SelectListItem { Text = "5", Value = "5" });
            _obj.Add(new SelectListItem { Text = "6", Value = "6" });
            _obj.Add(new SelectListItem { Text = "7", Value = "7" });
            _obj.Add(new SelectListItem { Text = "8", Value = "8" });
            _obj.Add(new SelectListItem { Text = "9", Value = "9" });
            _obj.Add(new SelectListItem { Text = "10", Value = "10" });

            ViewBag.Frequency = _obj;

            return _obj;
        }

        public IActionResult ActiveInactiveVisitor(string Visitor_id)
        {
            Cls_Visitor obj = new Cls_Visitor();
            obj._VAList = new List<Cls_Visitor>();
            DataTable dt = obj.Get_ActiveInActiveVisitorCards(Visitor_id, (HttpContext.Session.GetString("BaseLocation")));
            obj._VAList = BindActiveInActiveVisitor(dt);
            return View(obj);
        }
        public List<Cls_Visitor> BindActiveInActiveVisitor(DataTable dt)
        {
            List<Cls_Visitor> lst = new List<Cls_Visitor>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.Add(new Cls_Visitor()
                    {
                        //LineId = Convert.ToInt32(dr["LineID"].ToString()),

                        status = dr["status"].ToString(),
                        VisitorCardNo = dr["visitorcardno"].ToString(),

                    });
                }
            }
            return lst;
        }

    }
}
