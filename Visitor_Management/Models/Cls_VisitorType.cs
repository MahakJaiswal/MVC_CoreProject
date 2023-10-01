using Visitor_Management.Models.App_Code;
using System.Collections.Generic;
using System.Data;

namespace Visitor_Management.Models
{
    public class Cls_VisitorType : DataEntity
    {
        public List<Cls_VisitorType> _List { get; set; }


        public int LineId { get; set; }
        public string sysVisitorTypeUUid { get; set; }
        public string VisiotType { get; set; }
        public string PostedBy { get; set; }
        public string Posteddatetime { get; set; }
        public string Branchid { get; set; }


        public DataTable Select_VisitorType(string branchid)
        {
            return ExecuteDataSetFN("fn_getVisitorType", branchid).Tables[0];
        }


        public DataTable Insert_VisitorType(Cls_VisitorType _obj)
        {
            return ExecuteDataTableFN("Fn_Post_VisitorType", _obj.VisiotType, _obj.Branchid, _obj.PostedBy);
        }
    }
}