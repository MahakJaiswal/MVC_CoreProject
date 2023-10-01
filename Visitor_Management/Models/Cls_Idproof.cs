using Visitor_Management.Models.App_Code;
using System.Collections.Generic;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_IdProof : DataEntity
    {
        public List<Cls_IdProof> _List { get; set; }


        public int LineId { get; set; }
        public string sysIdproofUUid { get; set; }
        public string Idproof { get; set; }
        public string PostedBy { get; set; }
        public string Posteddatetime { get; set; }
        public string Branchid { get; set; }


        public DataTable Select_IdProof(string branchid)
        {
            return ExecuteDataSetFN("fn_getidproof", branchid).Tables[0];
        }

        public DataTable Insert_IdProof(Cls_IdProof _obj)
        {
            return ExecuteDataTableFN("fn_post_idproof", _obj.Idproof, _obj.Branchid, _obj.PostedBy);
        }
    }
}
