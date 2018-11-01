using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS_V2_MultipleTables.Member.ATT
{
    public class ATTEmployee
    {
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmpAddress { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhone { get; set; }
        public string EmpDOB { get; set; }
        public string EmpGender { get; set; }
        public string action { get; set; }

        private List<ATTQualification> _Qual = new List<ATTQualification>();

        public List<ATTQualification> Qual 
        {
            get { return _Qual; }
            set { _Qual = value; }
        }
    }
}