<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerCustomerDemoList.aspx.cs" Inherits="Northwind.Web.CustomerCustomerDemoList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View CustomerCustomerDemo</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="CustomerCustomerDemoForm" method="post" runat="server">
            <H2 align="center">CustomerCustomerDemo List</H2>
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
                            <a href='CustomerCustomerDemoEdit.aspx?CustomerID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "CustomerID" ).ToString() )%>&CustomerTypeID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "CustomerTypeID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='CustomerCustomerDemoView.aspx?CustomerID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "CustomerID" ).ToString() )%>&CustomerTypeID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "CustomerTypeID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="CustomerID" headertext="CustomerID" sortexpression="CustomerID" visible="True" />
                    <asp:boundcolumn datafield="CustomerTypeID" headertext="CustomerTypeID" sortexpression="CustomerTypeID" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='CustomerCustomerDemoEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
