<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="MIS_V2_MultipleTables.Employee" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>Employee</title>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"/>
    <style type="text/css">
        .data
        {
            padding: 5px 5px 5px 5px;
            }
        ::placeholder
        {
            color: #bcc0c6;
            font-family: Arial;
            font-size: 14px;
            
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="top" style="background-color:#dae1ed; color:#2b4960; font-family:Times New Roman" align="center">
        <h1 style="font-size:45px">Employee Detail</h1>
        <p style="font-size:20px">Enter employee Details</p>
    </div>

    <div class="container" style="font-size:17px; font-family:Times New Roman">
        <div class="row">
            <div class="col-md-6" >

                <table>
                    <tr>
                        <td class="data">Employee ID</td>
                        <td><asp:TextBox ID="EID" placeholder="Employee ID" style="color:#416191" runat="server" 
                                ontextchanged="EID_TextChanged"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="data">Employee Name</td>
                        <td ><asp:TextBox ID="EName" placeholder="Employee Name" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="data">Employee Address</td>
                        <td ><asp:TextBox ID="EAdd" placeholder="Employee Address" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="data">Employee Email</td>
                        <td><asp:TextBox ID="EEmail" placeholder="Employee Email" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="data">Employee Phone</td>
                        <td><asp:TextBox ID="EPhone" placeholder="Employee Phone" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="data">Employee DOB </td>
                        <td><asp:Calendar ID="DOB" runat="server" onselectionchanged="DOB_SelectionChanged"></asp:Calendar> 
                        <asp:TextBox ID="tbDOB" runat="server" Visible="False"></asp:TextBox> </td>
                    </tr>
                    <tr>
                        <td class="data">Employee Gender</td>
                        <td class="data"><asp:RadioButton ID="RadMale" Text="Male" runat="server" GroupName="Gender"></asp:RadioButton> 
                            <asp:RadioButton ID="RadFemale" Text="Female" runat="server" GroupName="Gender"></asp:RadioButton> 
                            <asp:RadioButton ID="Radothers" Text="Others" runat="server" GroupName="Gender"></asp:RadioButton> </td>
                    </tr>
                </table>
            </div>

            <div class="col-md-6">
                <%--<p align="left" style="font-size: large; font-weight: bold; color: #008000">Qualifiaction</p>--%>
                <table>
                    <tr>
                        <td  class="data">Qualification</td>
                        <td><asp:DropDownList ID="DrpDn" runat="server" 
                                style="margin-left:20px; margin-right:20px"></asp:DropDownList></td>
                        <td>Marks</td>
                        <td ><asp:TextBox runat="server" ID="TBMarks" style="margin-left:20px; margin-right:20px" placeholder="Marks"></asp:TextBox></td>
                        <td><asp:Button ID="BtnAdd" Text="Add" runat="server" style="background-color:#22529e; padding:6px 16px 6px 16px; color:White; border:none" onclick="BtnAdd_Click"></asp:Button></td>
                    </tr>
                </table>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                    onrowdatabound="GridView1_RowDataBound" 
                    onselectedindexchanged="GridView1_SelectedIndexChanged" Visible="False" >
                    <Columns>
                        <asp:BoundField DataField="QualID" HeaderText="QualID" />
                        <asp:BoundField DataField="QualName" HeaderText="QualName" />
                        <asp:BoundField DataField="Marks" HeaderText="Marks" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="LblRowindex" runat="server" Visible="False" ></asp:Label>
                <asp:Label ID="lblEmpAction" runat="server" Text="lblEmpAction" Visible="false"></asp:Label>
            </div>
        </div>
        <div class="row">
            <p  align="right">
                <asp:Button ID="btnSave" runat="server" text="Save" style="margin-left:30px; background-color:#22529e; color:White; border:none; margin:12px; padding:6px 16px 6px 16px" onclick="btnSave_Click"/>
                        <asp:Button ID="btnCancle" runat="server" style="margin-left:30px; background-color:#22529e; color:White; border:none; margin:12px; padding:6px 16px 6px 16px" Text="Cancel" onclick="btnCancle_Click" />
                        <asp:Button ID="btnUpdate" runat="server" 
                    style="margin-left:30px; background-color:#22529e; color:White; border:none; margin:12px; padding:6px 16px 6px 16px" 
                    Text="Update" visible="false" onclick="btnUpdate_Click"/>
                        <asp:Button ID ="btnDelete" runat = "server" Text = "Delete" style="margin-left:30px; background-color:#22529e; color:White; border:none; margin:12px; padding:6px 16px 6px 16px" onclick="btnDelete_Click" Visible="False" />
            </p>
        </div>
    </div>
    <p align="right">
        <asp:Label ID="lblUpdate" runat="server" Visible="false"></asp:Label>
    </p>
    </form>
</body>
</html>
