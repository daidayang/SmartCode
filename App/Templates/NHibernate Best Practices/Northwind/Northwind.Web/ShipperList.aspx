<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipperList.aspx.cs" Inherits="Northwind.Web.ShipperList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View Shipper</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="ShipperForm" method="post" runat="server">
            <H2 align="center">Shipper List</H2>
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
                            <a href='ShipperEdit.aspx?ShipperID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='ShipperView.aspx?ShipperID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="ID" headertext="ShipperID" sortexpression="ID" visible="True" />
                    <asp:boundcolumn datafield="CompanyName" headertext="CompanyName" sortexpression="CompanyName" visible="True" />
                    <asp:boundcolumn datafield="Phone" headertext="Phone" sortexpression="Phone" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='ShipperEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
