using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MIS_V2_MultipleTables.Member.ATT;
//using MIS_V2_MultipleTables.Member.BLL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MIS_V2_MultipleTables.Member.DLL
{
    public class DLLQualificationList
    {
        public List<ATTQualificationList> GetQualificationList(int? qualID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            List<ATTQualificationList> lst = new List<ATTQualificationList>();
            DataSet ds = new DataSet();
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("dbo.usp_Qualification_Select", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QualID", SqlDbType.Int).Value = qualID;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "QualificationList");

                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    ATTQualificationList obj = new ATTQualificationList();
                    obj.QualID = Int32.Parse(drow["QualID"].ToString());
                    obj.QualName = drow["QualName"].ToString();
                    lst.Add(obj);
                }
                return lst;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connect.Close();
            }

        }
    }
}