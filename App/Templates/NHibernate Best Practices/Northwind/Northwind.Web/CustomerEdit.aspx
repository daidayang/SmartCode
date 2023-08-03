<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerEdit.aspx.cs" Inherits="Northwind.Web.CustomerEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Customers Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Customers Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
            <table align="center">
                <tr>
                    <td class="label">CustomerID</td>
                    <td><asp:TextBox ID="uiCustomerID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">CompanyName</td>
                    <td><asp:TextBox ID="uiCompanyName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ContactName</td>
                    <td><asp:TextBox ID="uiContactName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ContactTitle</td>
                    <td><asp:TextBox ID="uiContactTitle" Runat="server" /></td>
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
                    <td class="label">Phone</td>
                    <td><asp:TextBox ID="uiPhone" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Fax</td>
                    <td><asp:TextBox ID="uiFax" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="CustomerList.aspx">View Customers List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiCustomerIDValidator" ControlToValidate="uiCustomerID" ErrorMessage="CustomerID is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiCompanyNameValidator" ControlToValidate="uiCompanyName" ErrorMessage="CompanyName is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
