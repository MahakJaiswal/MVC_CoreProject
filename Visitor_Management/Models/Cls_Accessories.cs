using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Accessories : DataEntity
    {
        public List<Cls_Accessories> _AccList { get; set; }
        public int LineId { get; set; }
        public string AccessoriesUUid { get; set; }
        public string AccessoriesName { get; set;}
        public string BranchId { get; set; }
        public string Postedby { get; set; }
        public string PostedDateTime { get; set; }

        public DataTable Select_Accessories(string brancid)
        {
            return ExecuteDataSetFN("fn_Get_accessories", brancid).Tables[0];
        }

        public DataTable Insert_Accessories(Cls_Accessories obj)
        {
            return ExecuteDataTableFN("Fn_Post_Accessories", obj.AccessoriesName, obj.BranchId, obj.Postedby);
        }
    }
}
