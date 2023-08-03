<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailList.aspx.cs" Inherits="Northwind.Web.OrderDetailList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View OrderDetail</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="OrderDetailForm" method="post" runat="server">
            <H2 align="center">OrderDetail List</H2>
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
                            <a href='OrderDetailEdit.aspx?OrderID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "OrderID" ).ToString() )%>&ProductID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ProductID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='OrderDetailView.aspx?OrderID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "OrderID" ).ToString() )%>&ProductID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ProductID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="OrderID" headertext="OrderID" sortexpression="OrderID" visible="True" />
                    <asp:boundcolumn datafield="ProductID" headertext="ProductID" sortexpression="ProductID" visible="True" />
                    <asp:boundcolumn datafield="UnitPrice" headertext="UnitPrice" sortexpression="UnitPrice" visible="True" />
                    <asp:boundcolumn datafield="Quantity" headertext="Quantity" sortexpression="Quantity" visible="True" />
                    <asp:boundcolumn datafield="Discount" headertext="Discount" sortexpression="Discount" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='OrderDetailEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
