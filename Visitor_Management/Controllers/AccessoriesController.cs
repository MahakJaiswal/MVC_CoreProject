using Microsoft.AspNetCore.Mvc;
using System.Data;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class AccessoriesController : Controller
    {
        public IActionResult Accessories()
        {
            Cls_Accessories _obj = new Cls_Accessories();
            _obj._AccList = new List<Cls_Accessories>();
            DataTable dt = _obj.Select_Accessories(HttpContext.Session.GetString("BaseLocation"));
            _obj._AccList = BindList(dt);
            return View(_obj);
        }

        public List<Cls_Accessories> BindList(DataTable dt)
        {
            int i = 0;
            List<Cls_Accessories> _locationlist = new List<Cls_Accessories>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    _locationlist.Add(new Cls_Accessories
                    {

                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        AccessoriesUUid = dr["SysAccessoriesUUid"].ToString(),
                        AccessoriesName = dr["Accessories"].ToString(),
                        BranchId = dr["BranchId"].ToString(),
                        Postedby = dr["FullName"].ToString(),
                        PostedDateTime = dr["PostedDateTime"].ToString(),
                        
                    });
                }
            }
            return _locationlist;
        }


        [HttpPost]

        public ActionResult Accessories(Cls_Accessories _objCM, string Save, string Update)
        {
            Cls_Accessories obj = new Cls_Accessories();

            if (ModelState.IsValid)
            {
                _objCM.Postedby = HttpContext.Session.GetString("SysAccountUUid");
                _objCM.BranchId = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(Save))
                {
                    //_objCM.lineid = 0;
                    DataTable dtSave = _objCM.Insert_Accessories(_objCM);
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

                DataTable dt = obj.Select_Accessories(HttpContext.Session.GetString("BaseLocation"));
                obj._AccList = BindList(dt);

            }
            return View(_objCM);
        }

        public IActionResult AddAccessories()
        {
            Cls_Accessories obj = new Cls_Accessories();
            return PartialView(obj);
        }

    }
}
