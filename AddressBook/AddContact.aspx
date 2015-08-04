<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="AddressBook.AddContact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Add new contact.</h2>
    
    <asp:DetailsView ID="AddContactDetailsView" runat="server"
                       AutoGenerateRows="False" DefaultMode="Insert" DataKeyNames="PersonId"
                       oniteminserting="AddContactDetailsView_ItemInserting"  
                       onmodechanging="AddContactDetailsView_ModeChanging" 
        CellPadding="3" CellSpacing="20" >
                              
           <Fields>
            <asp:DynamicField DataField="FirstName"  />
            <asp:DynamicField DataField="LastName"  />
            <asp:DynamicField DataField="City"  />
            <asp:DynamicField DataField="Country"  />
            <asp:DynamicField DataField="PhoneNumber"  />
            <asp:DynamicField DataField="Email"  />
            <asp:CommandField InsertText="Add New Contact" ShowInsertButton="True" />
          </Fields>            
    </asp:DetailsView>
    <asp:ValidationSummary ID="AddContactDetailsViewValidationSummary" runat="server" ShowSummary="true" DisplayMode="BulletList"/>

    <asp:Label ID="lblMessage" runat="server"></asp:Label>

</asp:Content>
