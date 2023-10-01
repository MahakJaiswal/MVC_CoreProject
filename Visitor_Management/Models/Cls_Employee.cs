using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Employee : DataEntity
    {

        public List<Cls_Employee> _EmpList { get; set; }
        public List<SelectListItem> DepartmentDDL { get; set; }
        public List<SelectListItem> DesignationDDL { get; set; }

        public int LineId { get; set; }
        public string EmpUUid { get; set; }
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Phone { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public string BranchId { get; set; }
        public string Status { get; set; }
        public string PostedBy { get; set; }
        public string postedFile { get; set; }
        public string PostedDatetime { get; set; }

        public DataTable Select_Employee(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Employee", branchid).Tables[0];
        }

        public DataTable Select_Department_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Department_DD", branchid).Tables[0];
        }

        public DataTable Insert_EmployeeList(Cls_Employee obj)
        {
            return ExecuteDataTableFN("Fn_Post_Employee", obj.EmpId, obj.EmpName, obj.Phone, obj.DepartmentId, obj.DesignationId, obj.BranchId, obj.PostedBy);
        }
    }
}
