<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="code.aspx.cs" Inherits="abrie.netWeb.code" ClientIDMode="Static"%>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>abrie.net - code</title>
    <meta name="keywords" content="abrie, greeff, code, examples" />
    <meta name="description" content="Code examples of ideas I play around with" />
    <link href="codecss" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <h1>
        Code</h1>
    <div class="codeNav">
        <a href="#" onclick="toggleArticle('#');">
            <img alt="back" src="/assets/img/back.png" /></a>
    </div>
    <div id="articles" runat="server">
    </div>
    <div id="comments">
    </div>
    <div id="newComment" class="newComment" style="display: none;">
        <div class="label">
            <p>
                Name</p>
            <input type="text" id="submitter" placeholder="nick" />
        </div>
        <div class="label">
            <p>
                Comment</p>
            <textarea id="comment" rows="5" placeholder="Comment"></textarea>
        </div>
        <br />
        <input type="button" value="Post" onclick="postComment();" />
    </div>
</asp:Content>
<asp:Content ID="footer" ContentPlaceHolderID="footer" runat="server">
    <script src="codejss"></script>
    <script>
        $(document).ready(function () {
            loadMarkup();
            if (window.location.hash == '' || window.location.hash == '#') {
                toggleArticle(window.location.hash);
            } else {
                toggleArticle(window.location.hash.slice(1));
            }
            SyntaxHighlighter.all();
        });
    </script>
</asp:Content>
