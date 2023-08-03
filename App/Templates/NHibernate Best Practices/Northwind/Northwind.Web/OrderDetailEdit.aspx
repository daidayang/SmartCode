<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailEdit.aspx.cs" Inherits="Northwind.Web.OrderDetailEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Order Details Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Order Details Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
            <table align="center">
                <tr>
                    <td class="label">OrderID</td>
                    <td><asp:TextBox ID="uiOrderID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ProductID</td>
                    <td><asp:TextBox ID="uiProductID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">UnitPrice</td>
                    <td><asp:TextBox ID="uiUnitPrice" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Quantity</td>
                    <td><asp:TextBox ID="uiQuantity" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Discount</td>
                    <td><asp:TextBox ID="uiDiscount" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="OrderDetailList.aspx">View Order Details List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiOrderIDValidator" ControlToValidate="uiOrderID" ErrorMessage="OrderID is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiProductIDValidator" ControlToValidate="uiProductID" ErrorMessage="ProductID is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiUnitPriceValidator" ControlToValidate="uiUnitPrice" ErrorMessage="UnitPrice is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiQuantityValidator" ControlToValidate="uiQuantity" ErrorMessage="Quantity is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiDiscountValidator" ControlToValidate="uiDiscount" ErrorMessage="Discount is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
