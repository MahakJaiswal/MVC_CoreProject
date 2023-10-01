using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Department : DataEntity
    {
        public List<Cls_Department> _DeptList { get; set; }

        public int LineId { get; set; }
        public string SysDepartmentUUid { get; set; }
        public string Department { get; set; }
        public string BranchId { get; set; }
        public string PosetdBy { get; set; }
        public string PostedDatetime { get; set; }

        public DataTable Select_Department(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_department", branchid).Tables[0];
        }

        public DataTable Insert_Department(Cls_Department obj)
        {
            return ExecuteDataTableFN("fn_post_department", obj.Department, obj.BranchId, obj.PosetdBy);
        }

    }
}
