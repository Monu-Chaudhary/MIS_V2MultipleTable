﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MIS_V2_MultipleTables.Member.ATT;
using System.Configuration;

namespace MIS_V2_MultipleTables.Member.DLL
{
    public class DLLEmployee
    {
        public bool SaveEmployee(ATTEmployee objEmployee)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            try
            {
                connect.Open();

                SqlTransaction trans;
                trans = connect.BeginTransaction();

                SqlCommand cmd = new SqlCommand("dbo.ups_Employee_Save", connect, trans);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int, 50).Value = objEmployee.EmpID;
                cmd.Parameters.Add("@EmpName", SqlDbType.VarChar, 50).Value = objEmployee.EmpName;
                cmd.Parameters.Add("@EmpEmail", SqlDbType.VarChar, 50).Value = objEmployee.EmpEmail;
                cmd.Parameters.Add("@EmpAddress", SqlDbType.VarChar, 50).Value = objEmployee.EmpAddress;
                cmd.Parameters.Add("@EmpPhone", SqlDbType.VarChar, 50).Value = objEmployee.EmpPhone;
                cmd.Parameters.Add("@EmpDOB", SqlDbType.Date, 50).Value = objEmployee.EmpDOB;
                cmd.Parameters.Add("@EmpGender", SqlDbType.VarChar, 50).Value = objEmployee.EmpGender;
                //Qualification ko save garne code

                try
                {
                    cmd.ExecuteNonQuery();


                    DLLQualification objbllqual = new DLLQualification();
                    bool ms = objbllqual.Save_Qualification(objEmployee.Qual, connect, trans);

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Error" + ex.Message);
                }

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

        public List<ATTEmployee> GetEmployee(int? empID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            SqlConnection connect = new SqlConnection(connectionstring);

            DataSet ds = new DataSet();
            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("dbo.usp_Employee_Select", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int).Value = empID;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                List<ATTEmployee> lst = new List<ATTEmployee>();
                if ((ds.Tables[0].Rows.Count) > 0)
                {
                    //DataRow drow = ds.Tables[0].Rows[0];
                    {
                        foreach (DataRow drow in ds.Tables[0].Rows)
                        {
                            ATTEmployee obj = new ATTEmployee();
                            obj.EmpID = Int32.Parse(drow["EmpID"].ToString());
                            obj.EmpName = drow["EmpName"].ToString();
                            obj.EmpEmail = drow["EmpEmail"].ToString();
                            obj.EmpAddress = drow["EmpAddress"].ToString();
                            obj.EmpPhone = drow["EmpPhone"].ToString();
                            obj.EmpDOB = drow["EmpDOB"].ToString();
                            obj.EmpGender = drow["EmpGender"].ToString();
                            lst.Add(obj);
                            obj.action = "E";
                        }
                    }
                }
                else
                {
                    ATTEmployee obj = new ATTEmployee();
                    obj.action = "A";
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
        public bool deleteEmployee(int? EmpID)
        {
            string connectionstring = ConfigurationManager.ConnectionStrings["DB"].ConnectionString ;
            SqlConnection connect = new SqlConnection(connectionstring) ;

            try
            {
                connect.Open();
                SqlCommand cmd = new SqlCommand("ups_Employee_Delete", connect);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@EmpID", SqlDbType.Int, 50).Value = EmpID;
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