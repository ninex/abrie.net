<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="notfound.aspx.cs" Inherits="abrie.netWeb.assets.errors.notfound" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <section style="position: relative; margin: 0px auto; width: 40%; top: 40px;">
        <p>
            <h2>
                welcome to abrie.net</h2>
            <br />
            You seem to have stumbled upon a page that doesn't exist. Use the navigation at
            the top to explore the site or just click <a href="/default.aspx">here</a> to go
            to the home page.</p><br />
    </section>
</asp:Content>
