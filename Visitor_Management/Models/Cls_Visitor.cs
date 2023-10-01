using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Operations;
using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Visitor : DataEntity
    {

        public List<Cls_Visitor>_VList { get; set; }
        public List<Cls_Visitor> _ExistingVList { get; set; }
        public string Date { get; set; }
        public int Count { get; set; }
        public int Month { get; set; }

        public List<Cls_Visitor> _VAList { get; set; }
        public List<Cls_Visitor> _VIAList { get; set; }

        public List<SelectListItem> EmployeeDDL { get; set; }
        public List<SelectListItem> DepartmentDDL { get; set; }
        public List<SelectListItem> VisitorTypeDDL { get; set; }
        public List<SelectListItem> VCardNoDDL { get; set; }
        public List<SelectListItem> PurposeDDL { get; set; }
        public List<SelectListItem> IdProofDDL { get; set; }
        public List<SelectListItem> AccessoriesDDL { get; set; }
        public List<SelectListItem> AdultDDL { get; set; }
        public List<SelectListItem> MinorsDDL { get; set; }

        public int LineId { get; set; }
        public string Visitor_Id { get; set; }
        public string Name { get; set; }
        public string EntryTime { get; set; }
        public string ExitTime { get; set; }
        public string Totalhours { get; set; }
        public string emailid { get; set; }
        public string phone { get; set; }
        public string idproofnumber { get; set; }
        public string companyname { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public string photo { get; set; }
        public string minors { get; set; }
        public string adults { get; set; }
        public string posteddatetime { get; set; }
        public string visitortype { get; set; }
        public string[] selvisitorcardno { get; set; }
        public string VisitorCardNo { get; set; }
        public string accessories { get; set; }
        public string idproof { get; set; }
        public string departmentname { get; set; }
        public string EmpName { get; set; }
        public string purpose_of_visit { get; set; }
        public string Postedby { get; set; }
        public string BranchId { get; set; }
        public string Present_Visitor { get; set; }
        public string Exit_Visitor { get; set; }
        public int No_Of_Visitor { get; set; }

        public DataTable Get_Visitor(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Visitor_test", branchid).Tables[0];
        }

        public DataTable Get_Employee_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Employee_DD", branchid).Tables[0];
        }

        public DataTable Get_Visitor_Type_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_VisitorType_DD", branchid).Tables[0];
        }

        public DataTable Get_VCardNo_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_VisitorCard_DD", branchid).Tables[0];
        }
        public DataTable Get_VCardNo_DDL(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_VisitorCard_DDL", branchid).Tables[0];
        }

        public DataTable Get_Department_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Department_DD", branchid).Tables[0];
        }

        public DataTable Get_PurposeOf_Visit_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Purpose_DD", branchid).Tables[0];
        }

        public DataTable Get_Accessories_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Accessories_DD", branchid).Tables[0];
        }

        public DataTable Get_Idproof_DD(string branchid)
        {
            return ExecuteDataSetFN("Fn_Get_Idproof_DD", branchid).Tables[0];
        }

        public DataTable Get_Employye_By_DepartmentId(string DeptUUid, string BranchId)
        {
            return ExecuteDataSetFN("Fn_Get_Emp_By_Department", DeptUUid, BranchId).Tables[0];
        }

        public DataTable Post_Visitor(Cls_Visitor obj)
        {

            var selVCardIds = String.Join(",", obj.selvisitorcardno);
            if (!string.IsNullOrEmpty(photo) && photo.Contains(","))
            {
                byte[] imageBytes = Convert.FromBase64String(photo.Split(',')[1]);
                obj.photo = Convert.ToBase64String(imageBytes);
            }

            else
            {
                obj.photo = "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";
            }
            return ExecuteDataTableFN("Fn_Post_Visitor", obj.BranchId, obj.Name, obj.emailid, obj.phone, obj.address, obj.adults, obj.minors, obj.companyname, obj.departmentname, obj.EmpName, obj.visitortype, selVCardIds, obj.accessories, obj.idproof, obj.idproofnumber, obj.purpose_of_visit, obj.Postedby, obj.photo);
        }
        public DataTable Select_VisitorCardsToExit(string visitor_id, string _sessionid)
        {
            return ExecuteDataSetFN("fn_select_visitorcardsbyid", visitor_id, _sessionid).Tables[0];
        }
        public DataTable Get_ActiveInActiveVisitorCards(string visitor_id, string branchid)
        {
            return ExecuteDataSetFN("fn_get_activeinactive_visitor", visitor_id, branchid).Tables[0];
        }
        public DataTable Get_ActiveVisitorCards(string visitor_id,string branchid)
        {
            return ExecuteDataSetFN("fn_get_active_visitorcards", visitor_id, branchid).Tables[0];
        }
        public DataTable Get_InActiveVisitorCards(string visitor_id, string branchid)
        {
            return ExecuteDataSetFN("fn_get_Inactive_visitorcards", visitor_id, branchid).Tables[0];
        }
      /*  public DataTable Update_VisitorStatus(Cls_Visitor _obj)
        {
            string[] vcardLiterals = _obj.selvisitorcardno.Select(vcard => $"'{vcard}'").ToArray();
            string vcardArrayLiteral = $"{{ {string.Join(",", vcardLiterals)} }}";

*//*            var VCardId = String.Join(",", _obj.selvisitorcardno);
*//*
            return ExecuteDataTableFN("fn_update_exittimebyidtry8", _obj.Visitor_Id, vcardArrayLiteral, _obj.BranchId);
        }*/
        public DataTable Update_VisitorStatus(Cls_Visitor _obj)
        {
/*            string[] vcardLiterals = _obj.selvisitorcardno.Select(vcard => $"'{vcard}'").ToArray();
            string vcardArrayLiteral = $"{{ {string.Join(",", vcardLiterals)} }}";*/

            var VCardId = String.Join(",", _obj.selvisitorcardno);

            return ExecuteDataTableFN("fn_update_visitor_status_testing", _obj.Visitor_Id, VCardId);
        }


        public DataTable Select_Existing_Visitor(string branchid)
        {
            return ExecuteDataSetFN("fn_get_Exisitingvisitordetails", branchid).Tables[0];
        }
        public DataTable Insert_ExistimngVisitor(Cls_Visitor obj)
        {
            var selVCardIds = String.Join(",", obj.selvisitorcardno);
            if (!string.IsNullOrEmpty(photo) && photo.Contains(","))
            {
                byte[] imageBytes = Convert.FromBase64String(photo.Split(',')[1]);
                obj.photo = Convert.ToBase64String(imageBytes);
            }

            else
            {
                obj.photo = "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";
            }
            return ExecuteDataTableFN("Fn_Post_Visitor", obj.BranchId, obj.Name, obj.emailid, obj.phone, obj.address, obj.adults, obj.minors, obj.companyname, obj.departmentname, obj.EmpName, obj.visitortype, selVCardIds, obj.accessories, obj.idproof, obj.idproofnumber, obj.purpose_of_visit, obj.Postedby, obj.photo);
        }
        public DataTable Select_VisitorDetails(string Visitor_id)
        {

            return ExecuteDataSetFN("fn_select_visitorbyid", Visitor_id).Tables[0];
        }

    }
}
