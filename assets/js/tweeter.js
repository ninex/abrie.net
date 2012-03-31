var last_id = 0;
var t;

function loadMarkup() {
    $.get('/assets/views/tweet.html', function (markup) {
        $.template("tweetTemplate", markup);
    });
}

function refresh() {
    var query = escape($('#search').val());

    var url = 'http://search.twitter.com/search.json?q=';
    var options = '&result_type=recent&rpp=5&callback=?&since_id=' + last_id;

    $.getJSON(url + query + options, function (json) {
        last_id = json.max_id_str;
        $.tmpl("tweetTemplate", json.results).prependTo("#results").hide().fadeIn(2000);
    });
    while ($('#results').children().size() > 8) {
        var last = $('#results').children().last().remove();
    }
    t = setTimeout("refresh()", 2000);
}

