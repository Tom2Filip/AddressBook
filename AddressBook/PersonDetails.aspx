<%@ Page Title="Person Details" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PersonDetails.aspx.cs" Inherits="AddressBook.PersonDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>


<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Person Details</h2>

     <asp:DetailsView ID="personDetailsView" runat="server"  
        AutoGenerateRows="False" DefaultMode="Edit" DataKeyNames="PersonId" 
         CellPadding="1" GridLines="None" 
         onmodechanging="personDetailsView_ModeChanging" onitemupdating="personDetailsView_ItemUpdating" >
            <Fields>        
            <asp:DynamicField DataField="FirstName" HeaderText="First Name"/>
            <asp:DynamicField DataField="LastName" HeaderText="Last Name" />
            <asp:DynamicField DataField="City" HeaderText="City" />
            <asp:DynamicField DataField="Country" HeaderText="Country" />
            <asp:DynamicField DataField="PhoneNumber" HeaderText="Phone #" />
            <asp:DynamicField DataField="Email" HeaderText="Email" />
            <asp:DynamicField DataField="AddedBy" ReadOnly="true" />
            <asp:CommandField UpdateText="Update" EditText="Edit" ShowEditButton="true" CancelText="Cancel" ShowCancelButton="true" />
         </Fields>
     </asp:DetailsView>
    <asp:ValidationSummary ID="personDetailsViewValidationSummary" runat="server" ShowSummary="true" 
                                DisplayMode="BulletList"/>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

</asp:Content>
