<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TerritoryList.aspx.cs" Inherits="Northwind.Web.TerritoryList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View Territory</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="TerritoryForm" method="post" runat="server">
            <H2 align="center">Territory List</H2>
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
                            <a href='TerritoryEdit.aspx?TerritoryID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='TerritoryView.aspx?TerritoryID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="ID" headertext="TerritoryID" sortexpression="ID" visible="True" />
                    <asp:boundcolumn datafield="TerritoryDescription" headertext="TerritoryDescription" sortexpression="TerritoryDescription" visible="True" />
                    <asp:boundcolumn datafield="RegionID" headertext="RegionID" sortexpression="RegionID" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='TerritoryEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
