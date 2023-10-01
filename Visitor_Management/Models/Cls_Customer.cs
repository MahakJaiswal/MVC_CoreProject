using System.Data;
using Visitor_Management.Models.App_Code;

namespace Visitor_Management.Models
{
    public class Cls_Customer : DataEntity
    {

        public List<Cls_Customer> _CustList { get; set; }

        public int LineId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPhone { get; set; }
        public string Photo { get; set; }
        public string Status { get; set; }
        public string Postedby { get; set; }
        public string CustUUid { get; set; }

        public DataTable Select_Customer(string sessionid)
        {
            return ExecuteDataSetFN("fn_get_customer", sessionid).Tables[0];
        }

        public DataTable Insert_Customer(Cls_Customer obj)
        {
            return ExecuteDataTableFN("fn_post_customer", obj.CustomerName, obj.CustomerEmail, obj.CustomerPhone,obj.Photo ,obj.Postedby);
        }

    }
}
