using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS_V2_MultipleTables.Member.ATT;
using MIS_V2_MultipleTables.Member.DLL;

namespace MIS_V2_MultipleTables.Member.BLL
{
    public class BLLQualificationList
    {
        public List<ATTQualificationList> GetQualificationList(int? qualID)
        {
            List<ATTQualificationList> lst = new List<ATTQualificationList>();
            try
            {
                DLLQualificationList dllQualList = new DLLQualificationList();
                lst = dllQualList.GetQualificationList(qualID);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                //throw (ex);
            }
            return lst;
        }
    }
}