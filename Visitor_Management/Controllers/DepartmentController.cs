using Microsoft.AspNetCore.Mvc;
using System.Data;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult MasterDepartment()
        {
            Cls_Department obj = new Cls_Department();
            obj._DeptList = new List<Cls_Department>();
            DataTable dt = obj.Select_Department((HttpContext.Session.GetString("BaseLocation")));
            obj._DeptList = BindDept(dt);
            return View(obj);
        }

        public List<Cls_Department> BindDept(DataTable dt)
        {
            List<Cls_Department> lst = new List<Cls_Department>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.Add(new Cls_Department()
                    {
                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        SysDepartmentUUid = dr["SysDepartmentUUid"].ToString(),
                        Department = dr["DepartmentName"].ToString(),
                        BranchId = dr["locationname"].ToString(),
                        PosetdBy = dr["FullName"].ToString(),
                        PostedDatetime = dr["PostedDateTime"].ToString(),
                    });
                }
            }
            return lst;
        }

        [HttpPost]
        public IActionResult MasterDepartment(Cls_Department _obj, Microsoft.AspNetCore.Http.IFormFile postedFile, IFormCollection form, string Save, string Upload, string Update, string Confirm)
        {

            List<Cls_Department> _uploadList = new List<Cls_Department>();
            if (ModelState.IsValid)
            {
                _obj.PosetdBy = HttpContext.Session.GetString("SysAccountUUid");
                _obj.BranchId = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(Save))
                {

                    // _obj.SysVisitorCardId = '0';
                    DataTable dtSave = _obj.Insert_Department(_obj);
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

                DataTable dt = _obj.Select_Department((HttpContext.Session.GetString("BaseLocation")));
                _obj._DeptList = BindDept(dt);
            }
            return View(_obj);
        }

        public IActionResult AddDepartment()
        {
            Cls_Department obj = new Cls_Department();
            return PartialView(obj);
        }
    }
}
