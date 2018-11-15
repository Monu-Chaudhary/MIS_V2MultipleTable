using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MIS_V2_MultipleTables.Member.ATT;
using System.Configuration;

namespace MIS_V2_MultipleTables.Member.DLL
{
    public class DLLQualification
    {
        public string Save_Qualification(ATTQualification objQual)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("dbo.usp_Qualification_Save", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int, 50).Value = objQual.EmpID;
                cmd.Parameters.Add("@QualID", SqlDbType.Int, 50).Value = objQual.QualID;
                cmd.Parameters.Add("@Marks", SqlDbType.Float, 50).Value = objQual.Marks;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connect.Close();
            }
            return "sucessfully saved qualification";
        }
    }
}