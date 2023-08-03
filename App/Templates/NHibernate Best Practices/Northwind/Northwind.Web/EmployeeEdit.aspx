<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeEdit.aspx.cs" Inherits="Northwind.Web.EmployeeEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Employees Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Employees Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
             <input type="hidden" id="uiEmployeeID" runat="server" />
            <table align="center">
                <tr>
                    <td class="label">LastName</td>
                    <td><asp:TextBox ID="uiLastName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">FirstName</td>
                    <td><asp:TextBox ID="uiFirstName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Title</td>
                    <td><asp:TextBox ID="uiTitle" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">TitleOfCourtesy</td>
                    <td><asp:TextBox ID="uiTitleOfCourtesy" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">BirthDate</td>
                    <td><asp:TextBox ID="uiBirthDate" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">HireDate</td>
                    <td><asp:TextBox ID="uiHireDate" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Address</td>
                    <td><asp:TextBox ID="uiAddress" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">City</td>
                    <td><asp:TextBox ID="uiCity" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Region</td>
                    <td><asp:TextBox ID="uiRegion" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">PostalCode</td>
                    <td><asp:TextBox ID="uiPostalCode" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Country</td>
                    <td><asp:TextBox ID="uiCountry" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">HomePhone</td>
                    <td><asp:TextBox ID="uiHomePhone" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Extension</td>
                    <td><asp:TextBox ID="uiExtension" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Notes</td>
                    <td><asp:TextBox ID="uiNotes" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ReportsTo</td>
                    <td><asp:TextBox ID="uiReportsTo" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">PhotoPath</td>
                    <td><asp:TextBox ID="uiPhotoPath" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="EmployeeList.aspx">View Employees List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiLastNameValidator" ControlToValidate="uiLastName" ErrorMessage="LastName is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiFirstNameValidator" ControlToValidate="uiFirstName" ErrorMessage="FirstName is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
