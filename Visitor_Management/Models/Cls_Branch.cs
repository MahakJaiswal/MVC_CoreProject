using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_BranchAdmin : DataEntity
    {
        public List<Cls_BranchAdmin> _List { get; set; }
        public List<SelectListItem> LocationDDL { get; set; }

        public int LineID { get; set; }
        public string SysBranchUUid { get; set; }
        public string BranchAdminName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string BranchID { get; set; }
        public string PostedBy { get; set; }
        public string Status { get; set; }
        public string Posteddatetime { get; set; }
        public string Accounttype { get; set; }
        

        public List<SelectListItem> StatusList { get; set; }



        public DataTable Select_MasterBranchAdmin(string sessionid)
        {
            return ExecuteDataSetFN("fn_get_branchadmin", sessionid).Tables[0];
        }

        public DataTable Post_MasterBranchAdmin(Cls_BranchAdmin _obj)
        {
            return ExecuteDataTableFN("fn_post_branchadmin", _obj.BranchAdminName, _obj.Email, _obj.Phone, _obj.BranchID, _obj.PostedBy);
        }

        public DataTable Select_BranchStatus(int lineid, string sessionid)
        {
            return ExecuteDataSetFN("fn_select_branchstatusbyid", lineid, sessionid).Tables[0];
        }

        public DataTable Select_LocationDDL(string sessionid)
        {
            return ExecuteDataSetFN("fn_get_location_drop", sessionid).Tables[0];
        }

        public DataTable Update_BranchStatus(Cls_BranchAdmin _obj)
        {
            return ExecuteDataTableFN("fn_update_branchstatus", _obj.LineID, _obj.Status, _obj.PostedBy);
        }

    }

    public class Cls_Location : DataEntity
    {
        public int LineId { get; set; }
        public string SysLocationuuid { get; set; }
        public string LocationName { get; set; }
        public string Address { get; set; }
        public string Shiftstart { get; set; }
        public string Shiftend { get; set; }
        public int Pincode { get; set; }
        public string postedBy { get; set; }
        public string PostedDate { get; set; }


        public List<Cls_Location> BaseLocationList { get; set; }

        public DataTable Select_LocationList(string sessionid)
        {
            return ExecuteDataSetFN("fn_select_location", sessionid).Tables[0];
        }
        public DataTable InsertLocationDetails(Cls_Location _obj)
        {
            return ExecuteDataTableFN("fn_post_location", _obj.LocationName, _obj.Address, _obj.Pincode, _obj.Shiftstart, _obj.Shiftend,_obj.postedBy);// function pending--
        }
    }
}
