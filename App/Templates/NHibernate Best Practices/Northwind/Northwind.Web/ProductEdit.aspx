<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEdit.aspx.cs" Inherits="Northwind.Web.ProductEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Products Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Products Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
             <input type="hidden" id="uiProductID" runat="server" />
            <table align="center">
                <tr>
                    <td class="label">ProductName</td>
                    <td><asp:TextBox ID="uiProductName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">SupplierID</td>
                    <td><asp:DropDownList ID="uiSupplierID" Runat="server" Width="200" /></td>
                </tr>
                <tr>
                    <td class="label">CategoryID</td>
                    <td><asp:DropDownList ID="uiCategoryID" Runat="server" Width="200" /></td>
                </tr>
                <tr>
                    <td class="label">QuantityPerUnit</td>
                    <td><asp:TextBox ID="uiQuantityPerUnit" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">UnitPrice</td>
                    <td><asp:TextBox ID="uiUnitPrice" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">UnitsInStock</td>
                    <td><asp:TextBox ID="uiUnitsInStock" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">UnitsOnOrder</td>
                    <td><asp:TextBox ID="uiUnitsOnOrder" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ReorderLevel</td>
                    <td><asp:TextBox ID="uiReorderLevel" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Discontinued</td>
                    <td><asp:CheckBox ID="uiDiscontinued" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="ProductList.aspx">View Products List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiProductNameValidator" ControlToValidate="uiProductName" ErrorMessage="ProductName is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
