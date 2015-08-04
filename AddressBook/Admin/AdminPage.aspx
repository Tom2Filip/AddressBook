<%@ Page Title="Administrator Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="AddressBook.Admin.AdminPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
         <hgroup class="title">
                <h1><%: Title %></h1>
              </hgroup>
    
    <h3>List of application users</h3>

    <asp:GridView ID="usersGridView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="WhiteSmoke" ShowHeaderWhenEmpty="true" AutoGenerateSelectButton="true"
                    DataKeyNames="UserName, Id"  EmptyDataText="There are no users." AllowSorting="true" OnRowDataBound="usersGridView_RowDataBound" OnRowDeleting="usersGridView_RowDeleting" OnSelectedIndexChanged="usersGridView_SelectedIndexChanged" OnPageIndexChanging="usersGridView_PageIndexChanging" OnSelectedIndexChanging="usersGridView_SelectedIndexChanging">
        <AlternatingRowStyle BackColor="WhiteSmoke"></AlternatingRowStyle>
        <Columns>
                      
            <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <%# Container.DataItemIndex + 1 %>
                        </ItemTemplate>
                    </asp:TemplateField>

        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%# Bind("UserName") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField> 

            <asp:TemplateField HeaderText="Id">
            <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%# Bind("Id") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

            <asp:TemplateField HeaderText="Role">
            <ItemTemplate>
        <asp:Label ID="Label3" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>

            <asp:CommandField ShowDeleteButton="True" DeleteText="Delete" HeaderText="Delete" ItemStyle-ForeColor="#CCC" >
                    <ItemStyle ForeColor="#000CCC"></ItemStyle>
            </asp:CommandField>
             
        </Columns>        
        <SelectedRowStyle BackColor="#99CCFF" />        
    </asp:GridView>

    <br />

    <asp:DetailsView ID="UserDetailsView" runat="server" HeaderText="User Details" AutoGenerateEditButton="True" AutoGenerateRows="False" DataKeyNames="Id" EmptyDataText="No user selected" 
                       DefaultMode="Edit"  Height="50px" Width="125px" CellPadding="4" ForeColor="#333333" GridLines="None" OnItemUpdating="UserDetailsView_ItemUpdating" OnModeChanging="UserDetailsView_ModeChanging">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <CommandRowStyle BackColor="#E2DED6" Font-Bold="True" />
        <EditRowStyle BackColor="#999999" />
        <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" />
        <Fields>            
                <asp:BoundField  DataField="Id" HeaderText="ID" ReadOnly="true" />
                <asp:BoundField  DataField="UserName" HeaderText="Name" ReadOnly="true" />
            <asp:BoundField  DataField="Email" HeaderText="Email" />
        </Fields>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
    </asp:DetailsView>


    <asp:PlaceHolder runat="server" ID="rolesCheckBoxes" Visible="false">            
          <div id="addToAdmin" runat="server">
            Add user to Administrator role: <asp:CheckBox ID="canEditRoleCheckBox" runat="server" />
            </div>
          <div id="removeFromAdmin" runat="server">
            Remove user from Administrator role: <asp:CheckBox ID="CheckBox2" runat="server" />
           </div>
       </asp:PlaceHolder>       
      
    <asp:Label ID="lblMessage" runat="server" ></asp:Label>

</asp:Content>
