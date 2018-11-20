using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MIS_V2_MultipleTables.Member.ATT;
using MIS_V2_MultipleTables.Member.BLL;
using System.Configuration;

namespace MIS_V2_MultipleTables.Member.DLL
{
    public class DLLQualification
    {
        public bool Save_Qualification(ATTQualification objQual)
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
            return true;
        }

        public List<ATTUser> SelectQualification(int EID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            List<ATTUser> lst = new List<ATTUser>();

            DataSet ds = new DataSet();
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("ups_EmployeeQualification_Select", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int, 50).Value = EID;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds, "Qual");
                foreach (DataRow drow in ds.Tables[0].Rows)
                {
                    ATTUser userQual = new ATTUser();
                    userQual.QualID = int.Parse(drow["QualID"].ToString());
                    userQual.Marks = drow["Marks"].ToString();
                    userQual.QualName = (drow["QualName"].ToString());
                    lst.Add(userQual);
                }
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            finally
            {
                connect.Close();
            }
            return lst;
        }

        //TODO not needed the below procedure
        public bool deleteQual(int EID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("usp_Qualification_Delete", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int, 50).Value = EID;
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
            return true;
        }
    }
}