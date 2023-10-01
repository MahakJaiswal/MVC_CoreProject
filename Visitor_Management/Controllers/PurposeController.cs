using Visitor_Management.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace Visitor_Management.Controllers
{
    public class PurposeController : Controller
    {
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public PurposeController(IWebHostEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }


        public IActionResult MasterVisitingPurpose()
        {
            Cls_Purpose _obj = new Cls_Purpose();
            _obj._List = new List<Cls_Purpose>();
            DataTable ds = _obj.Select_MasterPurpose((HttpContext.Session.GetString("BaseLocation")));
            _obj._List = BindData(ds);

            return View(_obj);
        }

        public List<Cls_Purpose> BindData(DataTable ds)
        {
            List<Cls_Purpose> lst = new List<Cls_Purpose>();
            if (ds != null && ds.Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Rows)
                {

                    lst.Add(new Cls_Purpose()
                    {
                        LineID = Convert.ToInt32(dr["Lineid"].ToString()),
                        SysVisitingUUid = dr["SysvisitingUUid"].ToString(),
                        purpose_of_visit = dr["purpose_of_visit"].ToString(),
                        Branchid = dr["locationname"].ToString(),
                        PostedBy = dr["FullName"].ToString(),
                        Posteddatetime = dr["Posteddatetime"].ToString(),
                    });
                }
            }
            return lst;
        }

        [HttpPost]
        public IActionResult MasterVisitingPurpose(Cls_Purpose _obj, Microsoft.AspNetCore.Http.IFormFile postedFile, IFormCollection form, string save, string Upload, string Update, string Confirm)
        {

            List<Cls_Purpose> _uploadList = new List<Cls_Purpose>();
            if (ModelState.IsValid)
            {
                _obj.PostedBy = HttpContext.Session.GetString("SysAccountUUid");
                _obj.Branchid = HttpContext.Session.GetString("BaseLocation");



                if (!string.IsNullOrEmpty(save))
                {

                    _obj.LineID = 0;
                    DataTable dtSave = _obj.Post_MasterPurpose(_obj);
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

                DataTable dt = _obj.Select_MasterPurpose((HttpContext.Session.GetString("BaseLocation")));
                _obj._List = BindData(dt);

            }
            return View(_obj);
        }

        public IActionResult AddMasterVisitingPurpose()
        {
            Cls_Purpose ep = new Cls_Purpose();

            return PartialView("AddMasterVisitingPurpose", ep);
        }

        public List<SelectListItem> BindVisiting(DataTable dt6)
        {
            List<SelectListItem> NameList = new List<SelectListItem>();
            if (dt6 != null && dt6.Rows.Count > 0)
            {
                //if (rType == "Trip")
                //{
                //    object[] objrow = new object[] { "0", "Select Vehicle" };
                //    DataRow toInsert = dt.NewRow();
                //    toInsert.ItemArray = objrow;
                //    dt.Rows.InsertAt(toInsert, 0);
                //}
                foreach (DataRow dr in dt6.Rows)
                {
                    NameList.Add(new SelectListItem
                    {
                        Text = dr["Value"].ToString(),
                        Value = dr["Value"].ToString(),
                        Selected = true

                    });
                }
            }
            return NameList;
        }
    }
}