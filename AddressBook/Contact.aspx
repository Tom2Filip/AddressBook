<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="AddressBook.Contact" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
       </hgroup>

    <section class="contact">
        <header>
            <span>You can contact me at:</span>
            <div><b>Email: tom.filipcic@gmail.com</b></div>
        </header>
    </section>

</asp:Content>