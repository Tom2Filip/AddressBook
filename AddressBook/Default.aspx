<%@ Page Title="Address Book" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AddressBook._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %> - Home page.</h1>
              </hgroup>          
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
  
     <h2>Welcome to Address Book</h2>
        
    <h3>List of Contacts</h3>

    <asp:GridView ID="contactsGridView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" ShowHeaderWhenEmpty="true" EmptyDataText="There are no contacts." 
                                        DataKeyNames="PersonId"  AllowSorting="true" OnRowDeleting="contactsGridView_RowDeleting" OnRowDataBound="contactsGridView_RowDataBound">
        <Columns>            
            <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

           <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# Bind("FirstName") %>'></asp:Label> <asp:Label ID="Label12" runat="server" Text='<%# Bind("LastName") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>

       <asp:TemplateField HeaderText="City">
            <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Bind("City") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Country">
            <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Country") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Phone">
            <ItemTemplate>
        <asp:Label ID="Label4" runat="server" Text='<%# Bind("PhoneNumber") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>

             <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
        <asp:Label ID="Label5" runat="server" Text='<%# Bind("Email") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>  
            
            <asp:TemplateField HeaderText="Added By">
            <ItemTemplate>
        <asp:Label ID="Label6" runat="server" Text='<%# Bind("AddedBy") %>'></asp:Label>
    </ItemTemplate>
        </asp:TemplateField>     

            <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" HeaderText="Delete" ItemStyle-ForeColor="#CCC" />
             
             <asp:HyperLinkField DataNavigateUrlFields="PersonId" HeaderText="Details"
                DataNavigateUrlFormatString="PersonDetails.aspx?personID={0}" 
                Text="View Details..." />

        </Columns>
    </asp:GridView>
        
    <asp:Label runat="server" ID="lblError"></asp:Label>

</asp:Content>
