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
        //TODO the below function is redundant
        //public bool SaveQualification (List<ATTQualification> objATTQual)
        //{
        //    bool msg = false;
        //    try
        //    {
        //        DLLQualification objqual = new DLLQualification();
        //        //foreach (ATTQualification objATTQuaification in objATTQual)
        //        {
        //            msg = objqual.Save_Qualification(objATTQual);
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        throw(ex);
        //    }
        //    return msg;
        //}

        public List<ATTUser> SelectQualification(int EID)
        {
            List<ATTUser> lst = new List<ATTUser>();
            try
            {
                DLLQualification objdllQual = new DLLQualification();
                lst = objdllQual.SelectQualification(EID);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return lst;
        }

        public bool deleteQual(int EID)
        {
            bool msg = false;
            try
            {
                DLLQualification objdllQual = new DLLQualification();
                msg = objdllQual.deleteQual(EID);
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            return msg;
        }
    }
}