using Visitor_Management.Models.App_Code;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Purpose : DataEntity
    {
        public List<Cls_Purpose> _List { get; set; }


        public int LineID { get; set; }
        public string SysVisitingUUid { get; set; }
        public string purpose_of_visit { get; set; }

        public string PostedBy { get; set; }
        public string Branchid { get; set; }

        public string Posteddatetime { get; set; }
        public List<SelectListItem> VisitingPurposeDDL { get; set; }




        public DataTable Select_MasterPurpose(string branchid)
        {
            return ExecuteDataSetFN("fn_get_masterPurpose", branchid).Tables[0];
        }

        public DataTable Post_MasterPurpose(Cls_Purpose _obj)
        {
            return ExecuteDataTableFN("fn_post_visitingpurpose", _obj.purpose_of_visit, _obj.Branchid, _obj.PostedBy);
        }

        public DataTable Select_VisitingDropDown(string sessionid)
        {
            return ExecuteDataSetFN("Fn_getmasterVisitingPurposeDrop", sessionid).Tables[0];
        }

    }
}