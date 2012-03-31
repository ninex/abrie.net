function setActiveNavigation() {
    var path = window.location.href;
    var index = path.lastIndexOf('/');
    if (index > -1) {
        path = path.substring(index + 1);
    }
    index = path.lastIndexOf('.aspx');
    if (index > -1) {
        path = path.substring(0, index);
    }
    $('#nav_' + path).addClass('active');
    if (path == '') {
        $('#nav_default').addClass('active');
    } else {
        if (!$('nav ul li a').hasClass('active')) {
            $('nav ul:last-child').append('<li><a class="active">sandbox:' + path + '</a></li>');
        }
    }
}