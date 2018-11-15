using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS_V2_MultipleTables.Member.ATT;
using MIS_V2_MultipleTables.Member.DLL;

namespace MIS_V2_MultipleTables.Member.BLL
{
    public class BLLQualification
    {
        public string SaveQualification (List<ATTQualification> objATTQual)
        {
            string msg = "";
            try
            {
                DLLQualification objqual = new DLLQualification();
                foreach (ATTQualification objATTQuaification in objATTQual)
                {
                    msg = objqual.Save_Qualification(objATTQuaification);
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        
    }
}