<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryEdit.aspx.cs" Inherits="Northwind.Web.CategoryEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Categories Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Categories Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
             <input type="hidden" id="uiCategoryID" runat="server" />
            <table align="center">
                <tr>
                    <td class="label">CategoryName</td>
                    <td><asp:TextBox ID="uiCategoryName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Description</td>
                    <td><asp:TextBox ID="uiDescription" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="CategoryList.aspx">View Categories List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiCategoryNameValidator" ControlToValidate="uiCategoryName" ErrorMessage="CategoryName is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
