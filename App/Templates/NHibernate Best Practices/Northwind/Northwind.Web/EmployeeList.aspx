<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="Northwind.Web.EmployeeList" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>View Employee</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
    <form id="EmployeeForm" method="post" runat="server">
            <H2 align="center">Employee List</H2>
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
                            <a href='EmployeeEdit.aspx?EmployeeID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                edit</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <a href='EmployeeView.aspx?EmployeeID=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, "ID" ).ToString() )%>'>
                                view</a>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:boundcolumn datafield="ID" headertext="EmployeeID" sortexpression="ID" visible="True" />
                    <asp:boundcolumn datafield="LastName" headertext="LastName" sortexpression="LastName" visible="True" />
                    <asp:boundcolumn datafield="FirstName" headertext="FirstName" sortexpression="FirstName" visible="True" />
                    <asp:boundcolumn datafield="Title" headertext="Title" sortexpression="Title" visible="True" />
                    <asp:boundcolumn datafield="TitleOfCourtesy" headertext="TitleOfCourtesy" sortexpression="TitleOfCourtesy" visible="True" />
                    <asp:boundcolumn datafield="BirthDate" headertext="BirthDate" sortexpression="BirthDate" visible="True" />
                    <asp:boundcolumn datafield="HireDate" headertext="HireDate" sortexpression="HireDate" visible="True" />
                    <asp:boundcolumn datafield="Address" headertext="Address" sortexpression="Address" visible="True" />
                    <asp:boundcolumn datafield="City" headertext="City" sortexpression="City" visible="True" />
                    <asp:boundcolumn datafield="Region" headertext="Region" sortexpression="Region" visible="True" />
                    <asp:boundcolumn datafield="PostalCode" headertext="PostalCode" sortexpression="PostalCode" visible="True" />
                    <asp:boundcolumn datafield="Country" headertext="Country" sortexpression="Country" visible="True" />
                    <asp:boundcolumn datafield="HomePhone" headertext="HomePhone" sortexpression="HomePhone" visible="True" />
                    <asp:boundcolumn datafield="Extension" headertext="Extension" sortexpression="Extension" visible="True" />
                    <asp:boundcolumn datafield="Photo" headertext="Photo" sortexpression="Photo" visible="True" />
                    <asp:boundcolumn datafield="Notes" headertext="Notes" sortexpression="Notes" visible="True" />
                    <asp:boundcolumn datafield="ReportsTo" headertext="ReportsTo" sortexpression="ReportsTo" visible="True" />
                    <asp:boundcolumn datafield="PhotoPath" headertext="PhotoPath" sortexpression="PhotoPath" visible="True" />
                </Columns>
            </asp:DataGrid>
            <table align="center">
                <tr>
                    <td>
                        <a href='EmployeeEdit.aspx'>Add</a>
                    </td>
                </tr>
            </table>
        </form>
</body>
</html>
