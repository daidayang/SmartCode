<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderEdit.aspx.cs" Inherits="Northwind.Web.OrderEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Orders Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Orders Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
             <input type="hidden" id="uiOrderID" runat="server" />
            <table align="center">
                <tr>
                    <td class="label">CustomerID</td>
                    <td><asp:TextBox ID="uiCustomerID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">EmployeeID</td>
                    <td><asp:TextBox ID="uiEmployeeID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">OrderDate</td>
                    <td><asp:TextBox ID="uiOrderDate" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">RequiredDate</td>
                    <td><asp:TextBox ID="uiRequiredDate" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShippedDate</td>
                    <td><asp:TextBox ID="uiShippedDate" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipVia</td>
                    <td><asp:TextBox ID="uiShipVia" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">Freight</td>
                    <td><asp:TextBox ID="uiFreight" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipName</td>
                    <td><asp:TextBox ID="uiShipName" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipAddress</td>
                    <td><asp:TextBox ID="uiShipAddress" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipCity</td>
                    <td><asp:TextBox ID="uiShipCity" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipRegion</td>
                    <td><asp:TextBox ID="uiShipRegion" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipPostalCode</td>
                    <td><asp:TextBox ID="uiShipPostalCode" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">ShipCountry</td>
                    <td><asp:TextBox ID="uiShipCountry" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="OrderList.aspx">View Orders List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
