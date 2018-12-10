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
            //string a = string.Empty;
            DrpDn.DataSource = objQualList.GetQualificationList(0);
            DrpDn.DataTextField = "QualName";
            DrpDn.DataValueField = "QualID";
            DrpDn.DataBind();
            //Response.Write(DrpDn.SelectedItem+"\n");
            //Response.Write(DrpDn.SelectedValue+"\n");
            //Response.Write(DrpDn.SelectedValue.ToString()+"\n");

            BLLEmployee objbllEmp = new BLLEmployee();
            List<ATTEmployee> lst = objbllEmp.GetEmployee(0);
            GridViewEmp.DataSource = lst;
            GridViewEmp.DataBind();

            ListBox1.DataSource = lst; // objbllEmp.GetEmployee(0);
            ListBox1.DataTextField = "EmpName";
            ListBox1.DataValueField = "EmpID";
            ListBox1.DataBind();


        }

        private void SetInitialRow()
        {

            int i = 0;
            LblRowindex.Text = i.ToString();
            lblEmpAction.Text = "A";

            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("QualID", typeof(string)));
            dt.Columns.Add(new DataColumn("QualName", typeof(string)));
            dt.Columns.Add(new DataColumn("Marks", typeof(string)));
            dr = null;
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
            GridView1.Visible = true;
            int i = Int32.Parse(LblRowindex.Text);
            if (ViewState["CurrentTable"] != null )
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];  //unboxing
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count>0 )
                {
                    bool ifExists = false;

                    foreach (DataRow dr in dtCurrentTable.Rows)
                    {
                        if ( dr["QualID"].ToString() == DrpDn.SelectedValue.ToString() )
                            ifExists = true;
                    }
                    if ((TBMarks.Text) !=  string.Empty)
                    {
                        if (!ifExists)
                        {
                            string ID = DrpDn.SelectedValue.ToString();
                            //////////
                            drCurrentRow = dtCurrentTable.NewRow();
                            dtCurrentTable.Rows.Add(drCurrentRow);
                            /////////
                            dtCurrentTable.Rows[i]["QualID"] = DrpDn.SelectedValue.ToString();
                            dtCurrentTable.Rows[i]["QualName"] = DrpDn.SelectedItem.ToString();
                            dtCurrentTable.Rows[i]["Marks"] = TBMarks.Text;
                            //drCurrentRow = dtCurrentTable.NewRow();
                            i++;
                            LblRowindex.Text = i.ToString();

                            //dtCurrentTable.Rows.Add(drCurrentRow);
                            ViewState["CurrentTable"] = dtCurrentTable;

                            GridView1.DataSource = dtCurrentTable;
                            GridView1.DataBind();
                        }
                        else if (ifExists) //&& EditQual.Text == "true")
                        {
                            for (int j = 0; j < dtCurrentTable.Rows.Count; j++)
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
            tbDOB.Visible = false;

            BLLEmployee objbllemp = new BLLEmployee();
            bool msg = objbllemp.SaveEmployee(objAttEmp);

            //bool ms = false;
            //if (msg == true)
            //{
            //    BLLQualification objbllqual = new BLLQualification();
            //    ms = objbllqual.SaveQualification(objAttEmp.Qual);
            //}
            //if (ms == false)
            //{
            //    //Response.Write("Qualification Error");
            //    bool message = objbllemp.deleteEmployee(objAttEmp.EmpID);
            //    Response.Write("<script>alert('Not sucessfully saved. Enter the Qualification')</script>");
            //}

            //if (msg)
            //    Response.Write("<script>alert('successfully written into db')</script>");
            //else
            //    Response.Write("<script>alert('Not sucessfully saved. Enter the Qualification')</script>");

            ScriptManager.RegisterClientScriptBlock(this, typeof(), "validation.js", "Check()", true);
            //Response.Redirect("Employee.aspx");
        }

        protected void DOB_SelectionChanged(object sender, EventArgs e)
        {
            tbDOB.Text = DOB.SelectedDate.ToString("yyyy-MM-dd");
            DOB.Visible = false;
            tbDOB.Visible = true;
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

            List<ATTEmployee> objemplst = new List<ATTEmployee>();
            ATTEmployee objattemp = new ATTEmployee();
            objattemp.action = "A";

            objemplst = objbllEmp.GetEmployee(eid);

            if (objemplst[0].action == "E")
            {
                lblEmpAction.Text = "E";
                //Response.Write(objemp.action);
                foreach (ATTEmployee objemp in objemplst)
                {

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
                }

                    DOB.Visible = false;
                    tbDOB.Visible = true;
                    btnDelete.Visible = true;
                    BtnAdd.Text = "Edit";
                    //BtnAdd.Visible = false;
                    btnSave.Visible = false;
                    GridView1.Visible = true;
                    btnUpdate.Visible = true;
                BLLQualification objbllQual = new BLLQualification();
                List<ATTUser> list;

                list = objbllQual.SelectQualification(eid);

                DataTable dt = new DataTable();
                dt.Columns.Add("QualID", typeof(string));
                dt.Columns.Add("QualName", typeof(string));
                dt.Columns.Add("Marks", typeof(string));
                DataRow dr;
                int i = 0;
                foreach (ATTUser user in list)
                {
                    dr = dt.NewRow();
                    dr["QualID"] = user.QualID.ToString();
                    dr["QualName"] = user.QualName.ToString();
                    dr["Marks"] = user.Marks.ToString();
                    dt.Rows.Add(dr);
                    i++;
                    LblRowindex.Text = i.ToString();
                }
                ViewState["CurrentTable"] = dt;


                GridView1.DataSource = list;
                GridView1.DataBind();

            }
            else if (objemplst[0].action == "A")
            {
                //EName.Text = "";
                //EPhone.Text = "";
                //EEmail.Text = "";
                //EAdd.Text = "";
                //tbDOB.Text = "";
                //TBMarks.Text = "";

                DOB.Visible = true;
                tbDOB.Visible = false;
                //BtnAdd.Visible = true;
                btnDelete.Visible = false;
                btnSave.Visible = true;
                GridView1.Visible = false;
                //btnUpdate.Visible = false;
                SetInitialRow();
            }

            //TODO
            //lblUpdate.Text = "Update";
            //SetInitialRow();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            BLLQualification objbllQual = new BLLQualification();
            bool msg = objbllQual.deleteQual(int.Parse(EID.Text.ToString()));
            if (msg == true)
            {
                BLLEmployee objbllEmp = new BLLEmployee();
                bool ms = objbllEmp.deleteEmployee(int.Parse(EID.Text.ToString()));
                Response.Redirect("Employee.aspx");
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("Employee.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            ATTEmployee objAttEmp = new ATTEmployee();
            objAttEmp.EmpID = int.Parse(EID.Text.ToString());

            //instead of deleting qual and then employee, beter to use update statement
            BLLQualification objbllQual = new BLLQualification();
            bool mg = objbllQual.deleteQual(int.Parse(EID.Text.ToString()));
            if (mg == true)
            {
                BLLEmployee objbllEmp = new BLLEmployee();
                bool ms = objbllEmp.deleteEmployee(int.Parse(EID.Text.ToString()));
            }

            objAttEmp.EmpName = EName.Text.ToString();
            objAttEmp.EmpPhone = EPhone.Text;
            objAttEmp.EmpEmail = EEmail.Text;
            if (RadMale.Checked)
                objAttEmp.EmpGender = "Male";
            else if (RadFemale.Checked)
                objAttEmp.EmpGender = "Female";
            else
                objAttEmp.EmpGender = "Others";
            objAttEmp.EmpDOB = tbDOB.Text;
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

            if (mg == true)
            {
                BLLEmployee objbllemp = new BLLEmployee();
                bool msg = objbllemp.SaveEmployee(objAttEmp);

                //bool ms = false;
                //if (msg == true)
                //{
                //    BLLQualification objbllqual = new BLLQualification();
                //    ms = objbllqual.SaveQualification(objAttEmp.Qual);

                //    if (ms == false)
                //    {
                //        //Response.Write("Qualification Error");
                //        bool message = objbllemp.deleteEmployee(objAttEmp.EmpID);
                //        Response.Write("<script>alert('Not sucessfully updated. Enter the Qualification')</script>");
                //    }
                //    else
                //    {
                //        Response.Write("<script>alert('successfully updated into DB')</script>");

                //    }
                //}
            }
            

            Response.Redirect("Employee.aspx");
        }

        protected void EmployeeGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow gr = GridViewEmp.SelectedRow;
            EID.Text = gr.Cells[0].Text;
            EName.Text = gr.Cells[1].Text;
            EAdd.Text = gr.Cells[2].Text;
            EEmail.Text = gr.Cells[3].Text;
            EPhone.Text = gr.Cells[4].Text;
            tbDOB.Text = gr.Cells[5].Text;
            if (gr.Cells[6].Text == "Male")
                RadMale.Checked = true;
            else if (gr.Cells[6].Text == "Female")
                RadFemale.Checked = true;
            else
                Radothers.Checked = true;

            
            List<ATTEmployee> objemplst = new List<ATTEmployee>();

            BLLEmployee objbllemp = new BLLEmployee();

            
            DOB.Visible = false;
            tbDOB.Visible = true;
            btnDelete.Visible = true;
            BtnAdd.Text = "Edit";
            //BtnAdd.Visible = false;
            btnSave.Visible = false;
            GridView1.Visible = true;
            btnUpdate.Visible = true;
            BLLQualification objbllQual = new BLLQualification();
            List<ATTUser> list;

            list = objbllQual.SelectQualification(int.Parse(gr.Cells[0].Text.ToString()));

            DataTable dt = new DataTable();
            dt.Columns.Add("QualID", typeof(string));
            dt.Columns.Add("QualName", typeof(string));
            dt.Columns.Add("Marks", typeof(string));
            DataRow dr;
            int i = 0;
            foreach (ATTUser user in list)
            {
                dr = dt.NewRow();
                dr["QualID"] = user.QualID.ToString();
                dr["QualName"] = user.QualName.ToString();
                dr["Marks"] = user.Marks.ToString();
                dt.Rows.Add(dr);
                i++;
                LblRowindex.Text = i.ToString();
            }
            ViewState["CurrentTable"] = dt;


            GridView1.DataSource = list;
            GridView1.DataBind();
        }

        protected void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            int eid = int.Parse(ListBox1.SelectedValue.ToString());
            //Response.Write("<script>alert('textchanged : ' )</script>");
            BLLEmployee objbllEmp = new BLLEmployee();

            List<ATTEmployee> objemplst = new List<ATTEmployee>();
            ATTEmployee objattemp = new ATTEmployee();
            //objattemp.action = "A";

            objemplst = objbllEmp.GetEmployee(eid);

            foreach (ATTEmployee objemp in objemplst)
            {

                EID.Text = objemp.EmpID.ToString();
                EName.Text = objemp.EmpName.ToString();
                EAdd.Text = objemp.EmpAddress.ToString();
                EPhone.Text = objemp.EmpPhone.ToString();
                EEmail.Text = objemp.EmpEmail.ToString();
                tbDOB.Text = objemp.EmpDOB.ToString();
                if (objemp.EmpGender.ToString() == "Male")
                    RadMale.Checked = true;
                else if (objemp.EmpGender.ToString() == "Female")
                    RadFemale.Checked = true;
                else
                    Radothers.Checked = true;
            }
            DOB.Visible = false;
            tbDOB.Visible = true;
            btnDelete.Visible = true;
            BtnAdd.Text = "Edit";
            //BtnAdd.Visible = false;
            btnSave.Visible = false;
            GridView1.Visible = true;
            btnUpdate.Visible = true;
            BLLQualification objbllQual = new BLLQualification();
            List<ATTUser> list;

            list = objbllQual.SelectQualification(eid);

            DataTable dt = new DataTable();
            dt.Columns.Add("QualID", typeof(string));
            dt.Columns.Add("QualName", typeof(string));
            dt.Columns.Add("Marks", typeof(string));
            DataRow dr;
            int i = 0;
            foreach (ATTUser user in list)
            {
                dr = dt.NewRow();
                dr["QualID"] = user.QualID.ToString();
                dr["QualName"] = user.QualName.ToString();
                dr["Marks"] = user.Marks.ToString();
                dt.Rows.Add(dr);
                i++;
                LblRowindex.Text = i.ToString();
            }
            ViewState["CurrentTable"] = dt;


            GridView1.DataSource = list;
            GridView1.DataBind();
        }
    }
}