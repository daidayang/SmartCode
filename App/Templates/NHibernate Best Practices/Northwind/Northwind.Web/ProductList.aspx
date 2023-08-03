<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="Northwind.Web.ProductList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View Product</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="ProductForm" method="post" runat="server">
            <H2 align="center">Product List</H2>
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
                            <a href='ProductEdit.aspx?ProductID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='ProductView.aspx?ProductID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="ID" headertext="ProductID" sortexpression="ID" visible="True" />
                    <asp:boundcolumn datafield="ProductName" headertext="ProductName" sortexpression="ProductName" visible="True" />
                    <asp:boundcolumn datafield="SupplierID" headertext="SupplierID" sortexpression="SupplierID" visible="True" />
                    <asp:boundcolumn datafield="CategoryID" headertext="CategoryID" sortexpression="CategoryID" visible="True" />
                    <asp:boundcolumn datafield="QuantityPerUnit" headertext="QuantityPerUnit" sortexpression="QuantityPerUnit" visible="True" />
                    <asp:boundcolumn datafield="UnitPrice" headertext="UnitPrice" sortexpression="UnitPrice" visible="True" />
                    <asp:boundcolumn datafield="UnitsInStock" headertext="UnitsInStock" sortexpression="UnitsInStock" visible="True" />
                    <asp:boundcolumn datafield="UnitsOnOrder" headertext="UnitsOnOrder" sortexpression="UnitsOnOrder" visible="True" />
                    <asp:boundcolumn datafield="ReorderLevel" headertext="ReorderLevel" sortexpression="ReorderLevel" visible="True" />
                    <asp:boundcolumn datafield="Discontinued" headertext="Discontinued" sortexpression="Discontinued" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='ProductEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
