using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS_V2_MultipleTables.Member.ATT;
using MIS_V2_MultipleTables.Member.DLL;

namespace MIS_V2_MultipleTables.Member.BLL
{
    public class BLLEmployee
    {
        public bool SaveEmployee(ATTEmployee objEmployee)
        {
            bool msg = false;
            try
            {
                DLLEmployee dlEmployee = new DLLEmployee();
                msg = dlEmployee.SaveEmployee(objEmployee);
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            return msg;
        }

        public ATTEmployee GetEmployee(int empID)
        {
            ATTEmployee obj = new ATTEmployee();
            try
            {
                DLLEmployee dlEmployee = new DLLEmployee();
                obj = dlEmployee.GetEmployee(empID);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
               // throw (ex);
            }
            return obj;
        }

        public bool deleteEmployee(int EmpID)
        {
            bool msg = false;

            try
            {
                DLLEmployee objdllEmp = new DLLEmployee();
                msg = objdllEmp.deleteEmployee(EmpID);
            }
            catch (Exception ex)
            {
                throw(ex);
            }
            return msg;
        }

    }
}