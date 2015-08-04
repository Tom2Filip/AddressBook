<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="AddressBook.Account2.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <div>
    <h4 style="font-size: medium">Register a new user</h4>
        <hr />
        <p>
            <asp:Literal runat="server" ID="StatusMessage" />
        </p>                
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="UserName">User name</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="UserName" />                
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" 
                                    CssClass="field-validation-error" ErrorMessage="The user name field is required." ValidationGroup="registerVldGroup" />
            </div>
        </div>
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="Password">Password</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="Password" TextMode="Password" />  
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" 
                                    CssClass="field-validation-error" ErrorMessage="The password field is required." ValidationGroup="registerVldGroup" />              
            </div>
        </div>
        <div style="margin-bottom:10px">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword">Confirm password</asp:Label>
            <div>
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword" 
                     CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The confirm password field is required." ValidationGroup="registerVldGroup" />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword" ValidationGroup="registerVldGroup"
         CssClass="field-validation-error" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />          
            </div>
        </div>
        <div>
            <div>
                <asp:Button runat="server" OnClick="CreateUser_Click" Text="Register" ValidationGroup="registerVldGroup" />
            </div>
        </div>
    </div>



</asp:Content>
