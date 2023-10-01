using Visitor_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

namespace Visitor_Management.Controllers
{
    public class VisitorTypeController : Controller
    {
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public VisitorTypeController(IWebHostEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }


        public IActionResult MasterVisitorType()
        {
            Cls_VisitorType _obj = new Cls_VisitorType();
            _obj._List = new List<Cls_VisitorType>();
            DataTable dt = _obj.Select_VisitorType((HttpContext.Session.GetString("BaseLocation")));
            _obj._List = BindData(dt);

            return View(_obj);


        }
        public List<Cls_VisitorType> BindData(DataTable dt)
        {
            List<Cls_VisitorType> lst = new List<Cls_VisitorType>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.Add(new Cls_VisitorType()
                    {
                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        sysVisitorTypeUUid = dr["sysVisitortypeUUid"].ToString(),
                        VisiotType = dr["VisitorType"].ToString(),
                        PostedBy = dr["FullName"].ToString(),
                        Posteddatetime = dr["PostedDateTime"].ToString(),
                    });
                }
            }
            return lst;
        }

        [HttpPost]
        public IActionResult MasterVisitorType(Cls_VisitorType _obj, Microsoft.AspNetCore.Http.IFormFile postedFile, IFormCollection form, string save, string Upload, string Update, string Confirm)
        {

            List<Cls_VisitorType> _uploadList = new List<Cls_VisitorType>();
            if (ModelState.IsValid)
            {
                _obj.PostedBy = HttpContext.Session.GetString("SysAccountUUid");
                _obj.Branchid = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(save))
                {

                    DataTable dtSave = _obj.Insert_VisitorType(_obj);
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


                DataTable dt = _obj.Select_VisitorType((HttpContext.Session.GetString("BaseLocation")));
                _obj._List = BindData(dt);

            }
            return View(_obj);
        }

        public IActionResult AddMasterVisitorType()
        {
            Cls_VisitorType ep = new Cls_VisitorType();
            return PartialView("AddMasterVisitorType", ep);
        }
    }
}