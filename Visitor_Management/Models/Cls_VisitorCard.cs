using Visitor_Management.Models.App_Code;
using System.Collections.Generic;
using System.Data;

namespace Visitor_Management.Models
{
    public class Cls_VisitorCard : DataEntity
    {
        public List<Cls_VisitorCard> _List { get; set; }

        public string SysVisitorCardId { get; set; }
        public string VisitorCardNo { get; set; }
        public string PostedBy { get; set; }
        public string PostedDateTime { get; set; }
        public string Branchid { get; set; }

        public DataTable Select_VisitorCardNo(string branchid)
        {
            return ExecuteDataSetFN("fn_get_visitorcardno", branchid).Tables[0];
        }

        public DataTable Post_VisitorCardNo(Cls_VisitorCard _obj)
        {
            return ExecuteDataTableFN("fn_post_visitor_card", _obj.VisitorCardNo, _obj.Branchid, _obj.PostedBy);

        }
    }
}