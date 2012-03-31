<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="test.aspx.cs" Inherits="abrie.netWeb.sandbox.test" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>abrie.net - test</title>
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <h1>
        Post to service</h1>
    <div style="margin: 10px;">
        <input type="button" value="Test" onclick="testClick();" />
        <div id="result">
        </div>
    </div>
    <script>
        function testClick() {
            $('#result').empty();
            $.ajax({
                type: "POST",
                url: '/WCF/Dataservice.svc/test',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#result').append('service is up');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $('#result').append(textStatus + ' / ' + errorThrown);
                }
            });
        }
    </script>
</asp:Content>
