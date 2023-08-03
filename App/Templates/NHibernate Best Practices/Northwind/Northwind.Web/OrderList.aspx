<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderList.aspx.cs" Inherits="Northwind.Web.OrderList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View Order</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="OrderForm" method="post" runat="server">
            <H2 align="center">Order List</H2>
            <table align="center">
                <tr>
                    <td><a href="Default.html">Home</a></td>
                </tr>
            </table>
            <br />
            <br />
            <asp:DataGrid Runat="server" ID="gridData" AllowPaging="True" PageSize="25"
                OnPageIndexChanged="ChangeGridPage" AutoGenerateColumns="False" HorizontalAlign="Center">
                <PagerStyle Position="Bottom" NextPageText="Next" PrevPageText="Previous" Mode="NextPrev" BackColor="#ffffff"></PagerStyle>
                <headerstyle cssclass="header" borderwidth="0" />
                <alternatingitemstyle cssclass="alternatingitemstyle" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Edit">
                        <ItemTemplate>
                            <a href='OrderEdit.aspx?OrderID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='OrderView.aspx?OrderID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="ID" headertext="OrderID" sortexpression="ID" visible="True" />
                    <asp:boundcolumn datafield="CustomerID" headertext="CustomerID" sortexpression="CustomerID" visible="True" />
                    <asp:boundcolumn datafield="EmployeeID" headertext="EmployeeID" sortexpression="EmployeeID" visible="True" />
                    <asp:boundcolumn datafield="OrderDate" headertext="OrderDate" sortexpression="OrderDate" visible="True" />
                    <asp:boundcolumn datafield="RequiredDate" headertext="RequiredDate" sortexpression="RequiredDate" visible="True" />
                    <asp:boundcolumn datafield="ShippedDate" headertext="ShippedDate" sortexpression="ShippedDate" visible="True" />
                    <asp:boundcolumn datafield="ShipVia" headertext="ShipVia" sortexpression="ShipVia" visible="True" />
                    <asp:boundcolumn datafield="Freight" headertext="Freight" sortexpression="Freight" visible="True" />
                    <asp:boundcolumn datafield="ShipName" headertext="ShipName" sortexpression="ShipName" visible="True" />
                    <asp:boundcolumn datafield="ShipAddress" headertext="ShipAddress" sortexpression="ShipAddress" visible="True" />
                    <asp:boundcolumn datafield="ShipCity" headertext="ShipCity" sortexpression="ShipCity" visible="True" />
                    <asp:boundcolumn datafield="ShipRegion" headertext="ShipRegion" sortexpression="ShipRegion" visible="True" />
                    <asp:boundcolumn datafield="ShipPostalCode" headertext="ShipPostalCode" sortexpression="ShipPostalCode" visible="True" />
                    <asp:boundcolumn datafield="ShipCountry" headertext="ShipCountry" sortexpression="ShipCountry" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='OrderEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
