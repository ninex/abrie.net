function loadMarkup() {
    $.get('/assets/views/article.html', function (markup) {
        $.template("articleTemplate", markup);
    });
    $.get('/assets/views/articleSummary.html', function (markup) {
        $.template("summaryTemplate", markup);
    });
    $.get('/assets/views/comment.html', function (markup) {
        $.template("commentTemplate", markup);
    });
}
function loadArticles() {
    $('#articles').empty();
    $('#articles').append('<img alt="loading" src="/assets/img/loader.gif" loop="infinite" class="loader" />');
    $.ajax({
        type: "GET",
        contentType: 'application/json',
        url: '/WCF/DataService.svc/articles',
        success: function (json) {
            $('#articles').empty();
            $.tmpl("summaryTemplate", json).appendTo("#articles");
            $.each($('[id^=created_]'), function (key, value) {
                var date = eval(value.innerHTML.replace(/\/Date\((.*?)\)\//gi, "new Date($1)"));
                var d = date.getDate();
                var day = (d < 10) ? '0' + d : d;
                var m = date.getMonth() + 1;
                var month = (m < 10) ? '0' + m : m;
                var yy = date.getYear();
                var year = (yy < 1000) ? yy + 1900 : yy;

                value.innerHTML = year + "/" + month + "/" + day;
            });

            $(".section").click(function () {
                window.location = $(this).find("a").attr("href");
                toggleArticle(window.location.hash.slice(1));
                return false;
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
            alert('Problem loading articles. Please refresh.');
        }
    });
}
function loadArticle(id) {
    $('#articles').empty();
    $('#articles').append('<img alt="loading" src="/assets/img/loader.gif" loop="infinite" class="loader" />');
    $.ajax({
        type: "GET",
        contentType: 'application/json',
        url: '/WCF/DataService.svc/article/' + id,
        success: function (json) {
            $('#articles').empty();
            $.tmpl("articleTemplate", json).appendTo("#articles");
            SyntaxHighlighter.highlight();
            loadComments(id);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(errorThrown);
            alert('Problem loading article.');
        }
    });
}
function toggleArticle(id) {
    $('#comments').hide();
    $('#newComment').hide();
    $('#comments').empty();
    if (id == '' || id == '#') {
        $('.codeNav').hide();
        loadArticles();
    } else {
        loadArticle(id);
        $('.codeNav').show();
    }
}
function loadComments(id) {
    $('#comments').hide();
    $('#newComment').hide();
    $.get('/WCF/DataService.svc/comments/' + id, function (json) {
        $.tmpl("commentTemplate", json).appendTo("#comments");
        $.each($('[id^=created_]'), function (key, value) {
            var date = eval(value.innerHTML.replace(/\/Date\((.*?)\)\//gi, "new Date($1)"));
            var d = date.getDate();
            var day = (d < 10) ? '0' + d : d;
            var m = date.getMonth() + 1;
            var month = (m < 10) ? '0' + m : m;
            var yy = date.getYear();
            var year = (yy < 1000) ? yy + 1900 : yy;
            var time = date.getHours() + ':' + date.getMinutes();

            value.innerHTML = time +' ' + year + "/" + month + "/" + day;
        });
    });
    $('#comments').fadeIn(3000);
    $('#newComment').fadeIn(3000);
}
function postComment() {
    var validationFailed = false;
    if ($('#submitter').val() == '') {
        $('#submitter').addClass('invalid');
        validationFailed = true;
    } else {
        $('#submitter').removeClass('invalid');
    }
    if ($('#comment').val() == '') {
        $('#comment').addClass('invalid');
        validationFailed = true;
    } else {
        $('#comment').removeClass('invalid');
    }

    if (validationFailed || window.location.hash == '') {
        return;
    }

    var id = window.location.hash.slice(1);

    if (id != null || '') {
        var data = {
            "Submitter": $('#submitter').val(),
            "Text": $('#comment').val(),
            "ArticleId": id
        };
        $.ajax({
            type: "POST",
            contentType: 'application/json',
            url: '/WCF/DataService.svc/comment',
            data: JSON.stringify(data),
            success: function (msg) {
                $('#title').val('');
                $('#content').val('');
                loadComments(window.location.hash.slice(1));
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    }
}