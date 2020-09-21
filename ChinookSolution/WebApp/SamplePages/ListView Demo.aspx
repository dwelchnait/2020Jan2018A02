<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListView Demo.aspx.cs" Inherits="WebApp.SamplePages.ListView_Demo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListView ID="TrackList" runat="server" DataSourceID="TrackListODS">
        <AlternatingItemTemplate>
            <tr style="">
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
              
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("TrackId") %>' runat="server" ID="TrackIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("MediaTypeId") %>' runat="server" ID="MediaTypeIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("GenreId") %>' runat="server" ID="GenreIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Composer") %>' runat="server" ID="ComposerTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Milliseconds") %>' runat="server" ID="MillisecondsTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Bytes") %>' runat="server" ID="BytesTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
               
            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" />
                    <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                </td>
                <td>
                    <asp:TextBox Text='<%# Bind("TrackId") %>' runat="server" ID="TrackIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Name") %>' runat="server" ID="NameTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("MediaTypeId") %>' runat="server" ID="MediaTypeIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("GenreId") %>' runat="server" ID="GenreIdTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Composer") %>' runat="server" ID="ComposerTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Milliseconds") %>' runat="server" ID="MillisecondsTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("Bytes") %>' runat="server" ID="BytesTextBox" /></td>
                <td>
                    <asp:TextBox Text='<%# Bind("UnitPrice") %>' runat="server" ID="UnitPriceTextBox" /></td>
               
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="">
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table runat="server" id="itemPlaceholderContainer" style="" border="0">
                            <tr runat="server" style="">
                                <th runat="server">TrackId</th>
                                <th runat="server">Name</th>
                                <th runat="server">AlbumId</th>
                                <th runat="server">MediaTypeId</th>
                                <th runat="server">GenreId</th>
                                <th runat="server">Composer</th>
                                <th runat="server">Milliseconds</th>
                                <th runat="server">Bytes</th>
                                <th runat="server">UnitPrice</th>
                               
                            </tr>
                            <tr runat="server" id="itemPlaceholder"></tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="">
                        <asp:DataPager runat="server" ID="DataPager1">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="">
                <td>
                    <asp:Label Text='<%# Eval("TrackId") %>' runat="server" ID="TrackIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Name") %>' runat="server" ID="NameLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("MediaTypeId") %>' runat="server" ID="MediaTypeIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("GenreId") %>' runat="server" ID="GenreIdLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Composer") %>' runat="server" ID="ComposerLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Milliseconds") %>' runat="server" ID="MillisecondsLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("Bytes") %>' runat="server" ID="BytesLabel" /></td>
                <td>
                    <asp:Label Text='<%# Eval("UnitPrice") %>' runat="server" ID="UnitPriceLabel" /></td>
                
            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="TrackListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Track_List" 
        TypeName="ChinookSystem.BLL.TrackController">

    </asp:ObjectDataSource>
</asp:Content>
