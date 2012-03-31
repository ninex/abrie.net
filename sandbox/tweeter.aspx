<%@ Page Title="" Language="C#" MasterPageFile="~/abrie.Master" AutoEventWireup="true"
    CodeBehind="tweeter.aspx.cs" Inherits="abrie.netWeb.sandbox.tweeter" %>

<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="server">
    <title>abrie.net - tweeter</title>
    <style>
        .tweet
        {
            border-radius: 10px;
            border: 1px solid #000;
            padding: 5px;
            background-color: #4685EB;
            margin-bottom: 10px;
            width: 600px;
        }
        .tweet:hover
        {
            background-color: #FFB5C7;
            cursor: pointer;
        }
        .table
        {
            display: table;
        }
        .row
        {
            display: table-row;
        }
        .cell
        {
            display: table-cell;
            padding-left: 5px;
            vertical-align: middle;
        }
    </style>
    <script src="http://ajax.microsoft.com/ajax/jquery.templates/beta1/jquery.tmpl.min.js"></script>
    <script src="/assets/js/tweeter.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="bodyContent" ContentPlaceHolderID="body" runat="server">
    <div style="margin: 10px;">
        <input type="text" id="search" placeholder="Search Term" />
        <input type="button" id="load" value="load" onclick="refresh();" />
    </div>
    <br />
    <div id="results">
    </div>
    <script>
        $(document).ready(function () {
            loadMarkup();
        });
    </script>
</asp:Content>
