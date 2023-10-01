using Autofac.Core;
using ClosedXML.Excel;
using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Data;
using Visitor_Management.Models;

namespace Visitor_Management.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        
        private IWebHostEnvironment Environment;
        private IConfiguration Configuration;
        public EmployeeController(IWebHostEnvironment _environment, IConfiguration _configuration, IWebHostEnvironment hostingEnvironment)
        {
            Environment = _environment;
            Configuration = _configuration;
            _hostingEnvironment = hostingEnvironment;
        }
        //public EmployeeController(IWebHostEnvironment hostingEnvironment)
        //{
        //    _hostingEnvironment = hostingEnvironment;
        //}

        public IActionResult EmployeeMaster()
        {
            Cls_Employee obj = new Cls_Employee();
            obj._EmpList = new List<Cls_Employee>();
            DataTable dt = obj.Select_Employee((HttpContext.Session.GetString("BaseLocation")));
            obj._EmpList = Bind_Employee(dt);
            return View(obj);
        }

        public List<Cls_Employee> Bind_Employee(DataTable dt)
        {
            List<Cls_Employee> lst = new List<Cls_Employee>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    lst.Add(new Cls_Employee()
                    {
                        LineId = Convert.ToInt32(dr["LineId"].ToString()),
                        EmpUUid = dr["SysEmployeeUUid"].ToString(),
                        EmpId = dr["EmpId"].ToString(),
                        EmpName = dr["EmpName"].ToString(),
                        Phone = dr["phone"].ToString(),
                        DepartmentId = dr["Department"].ToString(),
                        DesignationId = dr["Designation"].ToString(),
                        BranchId = dr["BranchId"].ToString(),
                        Status = dr["Status"].ToString(),
                        PostedBy = dr["FullName"].ToString(),
                        PostedDatetime = dr["PostedDateTime"].ToString(),
                    });
                }
            }
            return lst;
        }

        [HttpPost]
        public IActionResult EmployeeMaster(Cls_Employee obj, Microsoft.AspNetCore.Http.IFormFile postedFile, string upload, string Download, string Save)
        {
            //Cls_Employee _obj = new Cls_Employee();
            Cls_Employee _obj = new Cls_Employee();
            List<Cls_Employee> dls = new List<Cls_Employee>();

            if (ModelState.IsValid)
            {
                obj.PostedBy = HttpContext.Session.GetString("SysAccountUUid");
                obj.BranchId = HttpContext.Session.GetString("BaseLocation");

                if (!string.IsNullOrEmpty(Save))
                {
                    DataTable dtSave = _obj.Insert_EmployeeList(_obj);
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

                if (!string.IsNullOrEmpty(upload))
                {
                    if(postedFile!= null)
                    {
                        string wwwPath = this.Environment.WebRootPath;
                        string contentPath = this.Environment.ContentRootPath;

                        string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        string fileName = Path.GetFileName(postedFile.FileName);

                        using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                        {
                            postedFile.CopyTo(stream);

                            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                            using (var reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                reader.Read();
                                while (reader.Read()) //Each row of the file
                                {
                                    obj.EmpId = reader.GetValue(0).ToString();
                                    obj.EmpName = reader.GetValue(1).ToString();
                                    obj.Phone = reader.GetValue(2).ToString();
                                    obj.DepartmentId = reader.GetValue(3).ToString();
                                    obj.DesignationId = reader.GetValue(4).ToString();



                                    obj.Insert_EmployeeList(obj);

                                    dls.Add(new Models.Cls_Employee  //sending the filedata to model
                                    {
                                        EmpId = reader.GetValue(0).ToString(),
                                        EmpName = reader.GetValue(1).ToString(),
                                        Phone = reader.GetValue(2).ToString(),
                                        DepartmentId = reader.GetValue(3).ToString(),
                                        DesignationId = reader.GetValue(4).ToString(),


                                    });

                                }
                            }
                        }
                    }
                   
                }

                if (!string.IsNullOrEmpty(Download))
                {
                    var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "Upload", "Sample.xlsx");
                    var contentType = "application/octet-stream";
                    var fileName = Path.GetFileName(filePath);

                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                    return File(fileBytes, contentType, fileName);
                }

               
                DataTable dt = obj.Select_Employee((HttpContext.Session.GetString("BaseLocation")));
                obj._EmpList = Bind_Employee(dt);

            }

            return View(obj);
        }

        public IActionResult AddEmployee()
        {
            Cls_Employee obj = new Cls_Employee();
            DataTable dt = obj.Select_Department_DD((HttpContext.Session.GetString("BaseLocation")));

            obj.DepartmentDDL = BindDepartment(dt);
            return PartialView(obj);
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

    }

}
