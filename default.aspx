<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="abrie.netWeb._default" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>abrie.net - home</title>
    <script src="assets/js/default.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <h1>
        Home</h1>
    <div class="section">
        <img alt="me" src="/assets/img/ek.jpg" class="me" />
        <p>            
           Welcome to abrie.net, the home of Abrie Greeff on the net.<br /><br />
           I am a developer based in Cape Town, South Africa. I mostly code in C# and use most of the related .Net technologies. 
           I am also trying to make time to stay up to date with what is going on in web technology. <br />Currently I am focusing on improving my JavaScript, HTML5 and CSS3.
           As I am learning javascript I am also trying to focus on JSON and WCF.<br /><br />
           I will post code ideas on the site and if for some obscure reason you find yourself here, then I hope I have managed to teach you something.
           <br /><br />
           When I am not coding I spend my time mountain biking and watching football (tend to get a lot of flak for this, but I am a Manchester United supporter). 
        </p>
    </div>
    <div class="social">
        <a href="https://profiles.google.com/u/0/100493272651825902523">google+</a>
        <a href="http://www.facebook.com/agreeff">facebook</a>
        <a href="http://twitter.com/ninex">twitter</a>
        <a href="mailto:abrie@abrie.net">email</a>
    </div>
</asp:Content>
