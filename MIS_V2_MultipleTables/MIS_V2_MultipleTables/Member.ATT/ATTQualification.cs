using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIS_V2_MultipleTables.Member.ATT
{
    public class ATTQualification
    {
        public int QualID { get; set; }
        public int EmpID { get; set; }
        public string Marks { get; set; }
        public string Action { get; set; }
    }
}