<%@ Page Title="Repeater Demo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RepeaterDemo.aspx.cs" Inherits="WebApp.SamplePages.RepeaterDemo" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for Nested Query</h1>
    <blockquote>
        This page will demonstrate the Repeater control. This control is a great web
        control to display the structure of a DTO/POCO query. The control can be
        nested within itself to used to display the POCO component of the DTO structure.
        <br /><br />
        To ease the working with the properties in your class on this control
        use the ItemType attriute and assign the fully qualified class name of your
        data definition. The control uses a series of templates to fashion your display
    </blockquote>
    <div class="row">
        <div class="col-md-6 text-center">
            Enter the size of the playlist to view:&nbsp;&nbsp;
            <asp:TextBox ID="NumberOfTracks" runat="server"></asp:TextBox>&nbsp;&nbsp;
            <asp:Button ID="Submit" runat="server" Text="Submit" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-6 text-center">
            <asp:RequiredFieldValidator ID="RequiredNumberOfTracks" runat="server" 
                ErrorMessage="The playlist size is required" Display="None"
                 SetFocusOnError="true" ForeColor="Firebrick"
                 ControlToValidate="NumberOfTracks">
            </asp:RequiredFieldValidator>
<%--            <asp:CompareValidator ID="CompareNumberOfTracks" runat="server" 
                ErrorMessage="The playlist size must be a positive whole number" Display="None"
                 SetFocusOnError="true" ForeColor="Firebrick"
                 ControlToValidate="NumberOfTracks" Operator="GreaterThan" 
                 ValueToCompare="0" Type="Integer">
            </asp:CompareValidator>--%>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" />
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="offset-3">
            <%-- the repeater at the highest level gets its data
                using the DataSourceID attribute BECAUSE it is
                accessing a ODS control
                
                 add an ItemType to have access to the data definition
                  of your DTO. 
                to refer to the definitions you will use the key word 
                  Item.propertyname--%>
            <asp:Repeater ID="ClientPlayListDTO" runat="server"
                DataSourceID="ClientPlayListDTOODS"
                 ItemType="ChinookSystem.Data.DTOs.ClientPlayList">
                <HeaderTemplate>
                    <h2> Client Playlists for Requested Size</h2>
                </HeaderTemplate>
                <ItemTemplate>
                    <h3><%# Item.Name %>  (playtime: <%# Item.PlayTime %>)</h3>
                    <%-- the POCO list of data can be handled using
                         GridView, ListView, Repeater, ....
                        
                        the inner repeater gets its data
                        using the DataSource attribute BECAUSE it is
                        accessing the Item.Property of the record--%>
                    <asp:Repeater ID="SongList" runat="server"
                         DataSource='<%# Item.PlaylistSongs %>'
                         ItemType="ChinookSystem.Data.POCOs.PlayListSong">
                        <ItemTemplate>
                            <%# Item.SongName %> &nbsp;&nbsp; <%# Item.Genre %><br/>
                        </ItemTemplate>
                    </asp:Repeater>
                   <%-- <asp:GridView ID="GridView1" runat="server"
                         DataSource='<%# Item.PlaylistSongs %>'
                         ItemType="ChinookSystem.Data.POCOs.PlayListSong"></asp:GridView>--%>
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr style="height:3px; background-color:black" />
                </SeparatorTemplate>
                <FooterTemplate>
                    &copy Data is sensitive. Do not release
                </FooterTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:ObjectDataSource ID="ClientPlayListDTOODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Playlist_GetBySize" 
         OnSelected="SelectCheckForException"
        TypeName="ChinookSystem.BLL.PlayListController">
        <SelectParameters>
            <asp:ControlParameter ControlID="NumberOfTracks" 
                PropertyName="Text" DefaultValue="1" Name="playlistsize" 
                Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
