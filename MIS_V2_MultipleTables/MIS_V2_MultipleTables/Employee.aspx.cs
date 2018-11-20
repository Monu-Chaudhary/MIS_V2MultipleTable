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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               loadEmployeeDetail();
               SetInitialRow();
            }
        }
        protected void loadEmployeeDetail()
        {
            BLLEmployee objbllRelType = new BLLEmployee();

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

            int i = 0;
            LblRowindex.Text = i.ToString();
            lblEmpAction.Text = "A";

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
            if(lblEmpAction.Text == "A")
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
                if (dtCurrentTable.Rows.Count>0 )
                {
                    bool ifExists = false;

                    foreach (DataRow dr in dtCurrentTable.Rows)
                    {
                        if ( dr["QualID"].ToString() == DrpDn.SelectedValue.ToString() )
                            ifExists = true;
                    }

                    if(!ifExists)    
                        {
                            string ID = DrpDn.SelectedValue.ToString();
                            dtCurrentTable.Rows[i]["QualID"] = DrpDn.SelectedValue.ToString();
                            dtCurrentTable.Rows[i]["QualName"] = DrpDn.SelectedItem.ToString();
                            dtCurrentTable.Rows[i]["Marks"] = TBMarks.Text;
                            drCurrentRow = dtCurrentTable.NewRow();
                            i++;
                            LblRowindex.Text = i.ToString();

                            dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = dtCurrentTable;

                            GridView1.DataSource = dtCurrentTable;
                            GridView1.DataBind();
                        }
                    else if (ifExists) //&& EditQual.Text == "true")
                    {
                        for (int j = 0; j < dtCurrentTable.Rows.Count; j++ )
                            //foreach (DataRow dr in dtCurrentTable.Rows)
                            {
                                if (dtCurrentTable.Rows[j]["QualID"].ToString() == DrpDn.SelectedValue.ToString())
                                {
                                    dtCurrentTable.Rows[j]["QualID"] = DrpDn.SelectedValue.ToString();
                                    dtCurrentTable.Rows[j]["QualName"] = DrpDn.SelectedItem.ToString();
                                    dtCurrentTable.Rows[j]["Marks"] = TBMarks.Text;
                                    drCurrentRow = dtCurrentTable.NewRow();
                                    j++;
                                    //LblRowindex.Text = i.ToString();
                                    //Response.Write(LblRowindex);

                                    //dtCurrentTable.Rows.Add(drCurrentRow);
                                    ViewState["CurrentTable"] = dtCurrentTable;

                                    GridView1.DataSource = dtCurrentTable;
                                    GridView1.DataBind();
                                }
                            }
                    }
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
            bool msg = objbllemp.SaveEmployee(objAttEmp);

            bool ms = false;
            if (msg == true)
            {
                BLLQualification objbllqual = new BLLQualification();
                ms = objbllqual.SaveQualification(objAttEmp.Qual);
            }
            if (ms == false)
            {
                //Response.Write("Qualification Error");
                string message = objbllemp.deleteEmployee(objAttEmp.EmpID);
                Response.Write("<script>alert('Not sucessfully saved. Enter the Qualification')</script>");
            }
            else
            {
                Response.Write("<script>alert('successfully written into DB')</script>");
                
            }

            EID.Text = "";
            EName.Text = "";
            EPhone.Text = "";
            EEmail.Text = "";
            EAdd.Text = "";
            tbDOB.Text = "";
            TBMarks.Text = "";

            SetInitialRow();
        }

        protected void DOB_SelectionChanged(object sender, EventArgs e)
        {
            tbDOB.Text = DOB.SelectedDate.ToString("yyyy-MM-dd");
            DOB.Visible = false;
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GridView1.SelectedRow;
            DrpDn.SelectedValue = gr.Cells[0].Text;
            DrpDn.SelectedItem.Text = gr.Cells[1].Text;
            TBMarks.Text = gr.Cells[2].Text;
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GridView1, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to edit the row.";
            }
        }

        protected void EID_TextChanged(object sender, EventArgs e)
        {
            int eid = int.Parse(EID.Text.ToString());
            //Response.Write("<script>alert('textchanged : ' )</script>");
            BLLEmployee objbllEmp = new BLLEmployee();

            ATTEmployee objemp = new ATTEmployee();
            objemp.action = "A";
            
            objemp = objbllEmp.GetEmployee(eid);
            
            if (objemp.action == "E")
            {
                lblEmpAction.Text = "E";
                //Response.Write(objemp.action);
                EID.Text = objemp.EmpID.ToString();
                EName.Text = objemp.EmpName.ToString();
                EAdd.Text = objemp.EmpAddress.ToString();
                EPhone.Text = objemp.EmpPhone.ToString();
                EEmail.Text = objemp.EmpEmail.ToString();
                if (objemp.EmpGender.ToString() == "Male")
                    RadMale.Checked = true;
                else if (objemp.EmpGender.ToString() == "Female")
                    RadFemale.Checked = true;
                else
                    Radothers.Checked = true;
                //tbDOB.Text = DOB.SelectedDate.ToString("yyyy-MM-dd");
                tbDOB.Text = DateTime.Parse(objemp.EmpDOB).ToString("yyyy-MM-dd");
                DOB.Visible = false;
                
                btnDelete.Visible = true;
                BtnAdd.Visible = false;
                btnSave.Visible = false;

                BLLQualification objbllQual = new BLLQualification();
                List<ATTUser> list;

                list = objbllQual.SelectQualification(eid);
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            else if (objemp.action == "A")
            {
                EName.Text = "";
                EPhone.Text = "";
                EEmail.Text = "";
                EAdd.Text = "";
                tbDOB.Text = "";
                TBMarks.Text = "";

                DOB.Visible = true;
                btnDelete.Visible = false;
                SetInitialRow();
            } 
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee.aspx");
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //TODO delete
            BLLQualification objbllQual = new BLLQualification();
            bool msg = objbllQual.deleteQual(int.Parse(EID.Text.ToString()));
            if (msg == true)
            {
                BLLEmployee objbllEmp = new BLLEmployee();
                string ms = objbllEmp.deleteEmployee(int.Parse(EID.Text.ToString()));
                Response.Write("<script>alert" + ms + "</script>");
                Response.Redirect("Employee.aspx");
            }
        }
    }
}