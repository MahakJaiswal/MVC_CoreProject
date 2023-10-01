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
    public class IdProofController : Controller
    {
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public IdProofController(IWebHostEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }


        public IActionResult MasterIdProof()
        {
            Cls_IdProof _obj = new Cls_IdProof();
            _obj._List = new List<Cls_IdProof>();
            DataTable dt = _obj.Select_IdProof((HttpContext.Session.GetString("BaseLocation")));
            _obj._List = BindData(dt);

            return View(_obj);


        }
        public List<Cls_IdProof> BindData(DataTable dt)
        {
            List<Cls_IdProof> lst = new List<Cls_IdProof>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.Add(new Cls_IdProof()
                    {
                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        sysIdproofUUid = dr["sysIdproofUUid"].ToString(),
                        Idproof = dr["Idproof"].ToString(),
                        Branchid = dr["locationname"].ToString(),
                        PostedBy = dr["FullName"].ToString(),
                        Posteddatetime = dr["PostedDateTime"].ToString(),
                    });
                }
            }
            return lst;
        }

        [HttpPost]
        public IActionResult MasterIdProof(Cls_IdProof _obj, Microsoft.AspNetCore.Http.IFormFile postedFile, IFormCollection form, string save, string Upload, string Update, string Confirm)
        {

            List<Cls_IdProof> _uploadList = new List<Cls_IdProof>();
            if (ModelState.IsValid)
            {
                _obj.PostedBy = HttpContext.Session.GetString("SysAccountUUid");
                _obj.Branchid = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(save))
                {

                    // _obj.SysVisitorCardId = '0';
                    DataTable dtSave = _obj.Insert_IdProof(_obj);
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


                DataTable dt = _obj.Select_IdProof((HttpContext.Session.GetString("BaseLocation")));
                _obj._List = BindData(dt);

            }
            return View(_obj);
        }

        public IActionResult AddMasterIdProof()
        {
            Cls_IdProof ep = new Cls_IdProof();
            return PartialView("AddMasterIdProof", ep);
        }
    }
}