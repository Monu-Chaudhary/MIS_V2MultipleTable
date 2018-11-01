<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee.aspx.cs" Inherits="MIS_V2_MultipleTables.Employee" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>Employee ID</td>
                <td><asp:TextBox ID="EID" placeholder="Employee ID" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Employee Name</td>
                <td><asp:TextBox ID="EName" placeholder="Employee Name" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Employee Address</td>
                <td><asp:TextBox ID="EAdd" placeholder="Employee Address" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Employee Email</td>
                <td><asp:TextBox ID="EEmail" placeholder="Employee Email" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Employee Phone</td>
                <td><asp:TextBox ID="EPhone" placeholder="Employee Phone" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Employee DOB </td>
                <td><asp:Calendar ID="DOB" runat="server" onselectionchanged="DOB_SelectionChanged"></asp:Calendar> 
                <asp:TextBox ID="tbDOB" placeholder="Date of Birth" runat="server"></asp:TextBox> </td>
            </tr>
            <tr>
                <td>Employee Gender</td>
                <td><asp:RadioButton ID="RadMale" Text="Male" runat="server" GroupName="Gender"></asp:RadioButton> 
                    <asp:RadioButton ID="RadFemale" Text="Female" runat="server" GroupName="Gender"></asp:RadioButton> 
                    <asp:RadioButton ID="Radothers" Text="Others" runat="server" GroupName="Gender"></asp:RadioButton> </td>
            </tr>
        </table>
        <br />
        <%--<p align="left" style="font-size: large; font-weight: bold; color: #008000">Qualifiaction</p>--%>
        <table>
            <tr>
                <td>Qualification</td>
                <td><asp:DropDownList ID="DrpDn" runat="server" 
                        style="margin-left:20px; margin-right:20px"></asp:DropDownList></td>
                <td>Marks</td>
                <td ><asp:TextBox runat="server" ID="TBMarks" style="margin-left:20px; margin-right:20px"></asp:TextBox></td>
                <td><asp:Button ID="BtnAdd" Text="Add" runat="server" onclick="BtnAdd_Click" ></asp:Button></td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="QualID" HeaderText="QualID" />
                <asp:BoundField DataField="QualName" HeaderText="QualName" />
                <asp:BoundField DataField="Marks" HeaderText="Marks" />
            </Columns>
        </asp:GridView>
        <asp:Label ID="LblRowindex" runat="server" Visible="False" ></asp:Label>
        <p><asp:Button ID="btnSave" runat="server" text="Save" style="margin-left:30px" 
                onclick="btnSave_Click"/><asp:Button runat="server" style="margin-left:30px" Text="Cancel" /></p>
    </div>
    </form>
</body>
</html>
