<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegionEdit.aspx.cs" Inherits="Northwind.Web.RegionEdit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Region Edit</title>
        <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
        <link rel="stylesheet" href="Style.css" type="text/css">
</head>
<body>
<H2 align="center">Region Edit</H2>
        <table align="center">
            <tr>
                <td><a href="Default.html">Home</a></td>
            </tr>
        </table>
        <form id="form" method="post" runat="server">
            <table align="center">
                <tr>
                    <td class="label">RegionID</td>
                    <td><asp:TextBox ID="uiRegionID" Runat="server" /></td>
                </tr>
                <tr>
                    <td class="label">RegionDescription</td>
                    <td><asp:TextBox ID="uiRegionDescription" Runat="server" /></td>
                </tr>
            </table>
            <table align="center">
                <tr>
                    <td>
                        <asp:LinkButton Runat="server" ID="UpdateButton" OnClick="Update" Text="Save" />
                        <asp:LinkButton Runat="server" ID="DeleteButton" OnClick="Delete" Text="Delete" />
                        <a href="RegionList.aspx">View Region List</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="SaveLabel" runat="server" ForeColor="Red" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RequiredFieldValidator id="uiRegionIDValidator" ControlToValidate="uiRegionID" ErrorMessage="RegionID is a required field." runat="server" /><br />
                        <asp:RequiredFieldValidator id="uiRegionDescriptionValidator" ControlToValidate="uiRegionDescription" ErrorMessage="RegionDescription is a required field." runat="server" /><br />
                    </td>
                </tr>
                <tr style="visibility: hidden"><td><input type="checkbox" runat="server" id="uiIsNew" checked  /></td></tr>
            </table>
        </form>
</body>
</html>
