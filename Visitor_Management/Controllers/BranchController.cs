using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Visitor_Management.Models;
using static Visitor_Management.Models.Cls_BranchAdmin;

namespace Visitor_Management.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult MasterLocation()
        {
            Cls_Location _obj = new Cls_Location();
            _obj.BaseLocationList = new List<Cls_Location>();
            DataTable dt = _obj.Select_LocationList(HttpContext.Session.GetString("SysAccountUUid"));
            _obj.BaseLocationList = BindLocationList(dt);
            return View(_obj);
        }

        public List<Cls_Location> BindLocationList(DataTable dt)
        {
            int i = 0;
            List<Cls_Location> _locationlist = new List<Cls_Location>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    _locationlist.Add(new Cls_Location
                    {

                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        SysLocationuuid = dr["syslocationuuid"].ToString(),
                        LocationName = dr["locationName"].ToString().ToUpper(),
                        Address = dr["Address"].ToString().ToUpper(),
                        Pincode = Convert.ToInt32(dr["pincode"].ToString()),
                        Shiftstart = dr["Shiftstart"].ToString().ToUpper(),
                        Shiftend = dr["Shiftend"].ToString().ToUpper(),
                        postedBy = dr["postedby"].ToString(),
                        PostedDate = dr["posteddatetime"].ToString(),

                    });
                }
            }
            return _locationlist;
        }

        [HttpPost]

        public ActionResult MasterLocation(Cls_Location _objCM, string Save, string Update)
        {
            if (ModelState.IsValid)
            {
                _objCM.postedBy = HttpContext.Session.GetString("SysAccountUUid");
                if (!string.IsNullOrEmpty(Save))
                {
                    //_objCM.lineid = 0;
                    DataTable dtSave = _objCM.InsertLocationDetails(_objCM);
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

                DataTable dts = _objCM.Select_LocationList(HttpContext.Session.GetString("SysAccountUUid"));
                _objCM.BaseLocationList = BindLocationList(dts);

            }
            return View(_objCM);
        }

        public ActionResult AddMasterLocation()
        {
            Cls_Location _obj = new Cls_Location();
            return PartialView("AddMasterLocation", _obj);
        }

        public IActionResult MasterBranchAdmin()
        {
            Cls_BranchAdmin _obj = new Cls_BranchAdmin();
            _obj._List = new List<Cls_BranchAdmin>();
            DataTable dt = _obj.Select_MasterBranchAdmin(HttpContext.Session.GetString("SysAccountUUid"));
            _obj._List = BindBranchAdminList(dt);
            return View(_obj);
        }

        [HttpPost]

        public ActionResult MasterBranchAdmin(Cls_BranchAdmin _objCM, string Save, string Update)
        {
            if (ModelState.IsValid)
            {
                _objCM.PostedBy = HttpContext.Session.GetString("SysAccountUUid");
                if (!string.IsNullOrEmpty(Save))
                {
                    //_objCM.lineid = 0;
                    DataTable dtSave = _objCM.Post_MasterBranchAdmin(_objCM);
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

                DataTable dt = _objCM.Select_MasterBranchAdmin(HttpContext.Session.GetString("SysAccountUUid"));
                _objCM._List = BindBranchAdminList(dt);

            }
            return View(_objCM);
        }

        public List<Cls_BranchAdmin> BindBranchAdminList(DataTable dt)
        {
            int i = 0;
            List<Cls_BranchAdmin> _locationlist = new List<Cls_BranchAdmin>();
            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    i++;
                    _locationlist.Add(new Cls_BranchAdmin
                    {

                        LineID = Convert.ToInt32(dr["LineId"].ToString()),
                        SysBranchUUid = dr["SysBranchadminUUid"].ToString(),
                        BranchAdminName = dr["Name"].ToString().ToUpper(),
                        Email = dr["Email"].ToString().ToUpper(),
                        Phone = dr["Phone"].ToString().ToUpper(),
                        Password = dr["Password"].ToString(),
                        BranchID = dr["locationname"].ToString().ToUpper(),
                        PostedBy = dr["postedby"].ToString(),
                        //Posteddatetime = dr["posteddatetime"].ToString(),

                    });
                }
            }
            return _locationlist;
        }

        public ActionResult AddMasterBranchAdmin()
        {
            Cls_BranchAdmin _obj = new Cls_BranchAdmin();
            DataTable dt = _obj.Select_LocationDDL((HttpContext.Session.GetString("SysAccountUUid")));
            _obj.LocationDDL = BindLocation(dt);
            return PartialView("AddMasterBranchAdmin", _obj);
        }

        public List<SelectListItem> BindLocation(DataTable dt)
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
    }
}
