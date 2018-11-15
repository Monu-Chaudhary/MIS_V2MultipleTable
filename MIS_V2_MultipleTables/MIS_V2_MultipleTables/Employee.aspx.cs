using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MIS_V2_MultipleTables.Member.ATT;
using MIS_V2_MultipleTables.Member.BLL;
using System.Data;

namespace MIS_V2_MultipleTables
{
    public partial class Employee : System.Web.UI.Page
    {
        //public int rowIndex = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                int i = 0;
               LblRowindex.Text = i.ToString();
               loadEmployeeDetail();
               SetInitialRow();
            }
        }
        protected void loadEmployeeDetail()
        {
            List<ATTEmployee> msg;
            BLLEmployee objbllRelType = new BLLEmployee();

            msg = objbllRelType.GetEmployee(1);

            BLLQualificationList objQualList = new BLLQualificationList();

            DrpDn.DataSource = objQualList.GetQualificationList(1);
            DrpDn.DataTextField = "QualName";
            DrpDn.DataValueField = "QualID";
            DrpDn.DataBind();
            //Response.Write(DrpDn.SelectedItem+"\n");
            //Response.Write(DrpDn.SelectedValue+"\n");
            //Response.Write(DrpDn.SelectedValue.ToString()+"\n");

        }

        private void SetInitialRow()
        {
            

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add(new DataColumn("QualID", typeof(string)));
            dt.Columns.Add(new DataColumn("QualName", typeof(string)));
            dt.Columns.Add(new DataColumn("Marks", typeof(string)));
            dr = dt.NewRow();
            dr["QualID"] = string.Empty;
            dr["QualName"] = string.Empty;
            dr["Marks"] = string.Empty;
            dt.Rows.Add(dr);

            ViewState["CurrentTable"] = dt;

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            AddNewRowToGrid();
         }

        private void AddNewRowToGrid()
        {

            int i = Int32.Parse(LblRowindex.Text);
            //Response.Write(rowIndex);
            if (ViewState["CurrentTable"] != null )
            {
                DataTable dtCurrentTable = (DataTable) ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count>0)
                {
                    //for (int i=1; i<=dtCurrentTable.Rows.Count; i++)
                    {
                        Response.Write(DrpDn.SelectedValue.ToString().GetType() +"\n\t");
                        Response.Write(DrpDn.SelectedItem.ToString().GetType());
                        dtCurrentTable.Rows[i]["QualID"] = DrpDn.SelectedValue.ToString();
                        dtCurrentTable.Rows[i]["QualName"] = DrpDn.SelectedItem.ToString();
                        dtCurrentTable.Rows[i]["Marks"] = TBMarks.Text;
                        drCurrentRow = dtCurrentTable.NewRow();
                        i++;
                        LblRowindex.Text = i.ToString();
                        //Response.Write(LblRowindex);
                    }
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    GridView1.DataSource = dtCurrentTable;
                    GridView1.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ATTEmployee objAttEmp = new ATTEmployee();

            objAttEmp.EmpID = Convert.ToInt32(EID.Text.ToString());
            objAttEmp.EmpName = EName.Text;
            objAttEmp.EmpPhone = EPhone.Text;
            objAttEmp.EmpEmail = EEmail.Text; 
            if (RadMale.Checked)
                objAttEmp.EmpGender = "Male";
            else if (RadFemale.Checked)
                objAttEmp.EmpGender = "Female";
            else
                objAttEmp.EmpGender = "Others";

            //objAttEmp.EmpDOB = (DOB.SelectedDate.ToString());
            objAttEmp.EmpDOB = tbDOB.Text;
            //Response.Write(DOB.SelectedDate.ToString());
            objAttEmp.EmpAddress = EAdd.Text;

            foreach (GridViewRow row in GridView1.Rows)
            {
                ATTQualification qual = new ATTQualification();
                qual.EmpID = int.Parse(EID.Text.ToString());
                if (row.Cells[0].Text == "&nbsp;")
                {
                      break;
                }
                else
                {
                    qual.QualID = Convert.ToInt32(row.Cells[0].Text);
                }
                qual.Marks = (row.Cells[2].Text);
                qual.Action = "";
                objAttEmp.Qual.Add(qual);
                //Response.Write(objAttEmp.Qual);
            }
            DOB.Visible = true;
            BLLEmployee objbllemp = new BLLEmployee();
            string msg = objbllemp.SaveEmployee(objAttEmp);

            BLLQualification objbllqual = new BLLQualification();
            string ms = objbllqual.SaveQualification(objAttEmp.Qual);
        }

        protected void DOB_SelectionChanged(object sender, EventArgs e)
        {
            tbDOB.Text = DOB.SelectedDate.ToString("yyyy-MM-dd");
            DOB.Visible = false;
        }
       
    }
}