<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="articles.aspx.cs" Inherits="abrie.netWeb.sandbox.articles" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <script src="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <div style="margin-left: 5px;">
        <div class="table row">
            <div class="cell">
                <p>
                    Password</p>
                <input id="pwd" type="password" placeholder="Password" />
            </div>
            <div class="cell">
                <p>
                    Title</p>
                <input id="title" type="text" placeholder="Title of article" />
            </div>
            <div class="cell">
                <p>
                    Demo Link</p>
                <input id="demoLink" type="text" placeholder="Link to demo" />
            </div>
            <div class="cell" style="vertical-align: bottom;">
                <input id="Store" type="button" value="Store Article" onclick="storeArticle();" />
                <input id="articleId" type="hidden" />
            </div>
        </div>
        <br />
        <div>
            <p>
                Article Summary</p>
            <textarea id="summary" rows="5" placeholder="Article Summary" style="width: 95%;"></textarea></div>
        <p>
            Article Content</p>
        <textarea id="content" rows="20" placeholder="Article markup" style="width: 95%;"></textarea>
    </div>
    <div id="stored">
    </div>
    <script>
        $(document).ready(function () {
            loadMarkup();
            loadArticles();
            $('#articleId').val('');
        });
        function loadMarkup() {
            $.get('/assets/views/articleAdmin.html', function (markup) {
                $.template("articleTemplate", markup);
            });
        }
        function storeArticle() {
            var pwd = $('#pwd').val();
            var data = {
                "Title": $('#title').val(),
                "ArticleContent": $('#content').val(),
                "DemoLink": $('#demoLink').val(),
                "Id": $('#articleId').val(),
                "Summary": $('#summary').val()
            };
            $.ajax({
                type: "POST",
                contentType: 'application/json',
                url: '/WCF/DataService.svc/article?pwd=' + pwd,
                data: JSON.stringify(data),
                success: function (msg) {
                    $('#title').val('');
                    $('#content').val('');
                    $('#demoLink').val('');
                    $('#articleId').val('');
                    $('#summary').val('');
                    loadArticles();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
        function editArticle(id) {
            window.scrollTo(0, 0);
            $.getJSON('/WCF/Dataservice.svc/article/' + id, function (json) {
                $('#title').val(json.Title);
                $('#content').val(json.ArticleContent);
                $('#demoLink').val(json.DemoLink);
                $('#articleId').val(json.Id);
                $('#summary').val(json.Summary);
            });
        }
        function removeArticle(id) {
            var pwd = $('#pwd').val();
            $.ajax({
                type: "DELETE",
                contentType: 'application/json',
                url: '/WCF/DataService.svc/articles/' + id + '?pwd=' + pwd,
                success: function (msg) {
                    loadArticles();
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert(errorThrown);
                }
            });
        }
        function loadArticles() {
            $('#stored').empty();
            $.getJSON('/WCF/Dataservice.svc/articles', function (json) {
                $.tmpl("articleTemplate", json).appendTo("#stored");
            });
        }
    </script>
</asp:Content>
