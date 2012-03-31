
var canvasLength = 5, supportCanvas = true;
var slides,
    current = 0,
    slideshow = { width: 0, height: 0 },
    images = [],
    startIndex = 0,
    hidden = false,
    loaded = 0;

$(document).ready(function () {
    supportCanvas = 'getContext' in document.createElement('canvas');
    if (supportCanvas) {
        var canvas = $('#thumbs canvas')[0];
        canvas.onmousedown = thumbDown;
        $('#slideshow').hover(function () {
            $('#nav').fadeOut(200)
            $('#tips').fadeIn(500);
        },
        function () {
            $('#tips').fadeOut(200);
            $('#nav').fadeIn(1000);
        });
        $('#thumbs canvas').hover(function () {
            $('#nav').fadeOut(200)
            $('#tips').fadeIn(1000);
        },
        function () {
            $('#tips').fadeOut(200);
            $('#nav').fadeIn(1000);
        });
        document.onkeydown = keypressed;
        window.onresize = resize;
    }

    $('#navwork').click(function () {
        $('#about').fadeOut('slow');
        $('#contact').fadeOut('slow');
        $('#slideshow').slideDown('slow');
        $('#thumbs').animate({
            opacity: '100%',
            width: '140px'
        }, 'slow');
        hidden = false;
    });
    $('#navabout').click(function () {
        $('#contact').fadeOut('slow', function () {
            $('#slideshow').slideUp('slow');
            $('#thumbs').animate({
                width: '0%',
                opacity: '0%'
            }, 'slow');
            $('#about').fadeIn('slow');
        });
        hidden = true;
    });
    $('#navcontact').click(function () {
        $('#about').slideUp('slow');
        $('#slideshow').slideUp('slow');
        $('#thumbs').animate({
            width: '0%',
            opacity: '0%'
        }, 'slow');
        $('#contact').fadeIn('slow');
        hidden = true;
    });
})

$(window).load(function () {

    // We are listening to the window.load event, so we can be sure
    // that the images in the slideshow are loaded properly.

    // Testing wether the current browser supports the canvas element:
    supportCanvas = 'getContext' in document.createElement('canvas');

    // The canvas manipulations of the images are CPU intensive,
    // this is why we are using setTimeout to make them asynchronous
    // and improve the responsiveness of the page.
    slides = $('#slideshow li');

    setTimeout(function () {
        slideshow.width = $('.slides').width(); //820;
        slideshow.height = $('.slides').height(); // 620;
        if (supportCanvas) {
            $('#slideshow img').each(function () {
                // Rendering the modified versions of the images:
                createCanvasOverlay(this);
                if ($(this).hasClass('slideActive')) {
                    $('#desc').html($(this).attr('alt'));
                    $('#desc').slideDown('slow');
                }
            });
            drawThumbnails();
        } else {
            $('#slideshow img').css('display', 'block');
            $('#slideshow .loading').hide();
            $('#thumbs').hide();
        }
        $('#slideshow .arrow').click(arrowClick);

    }, 100);

    function arrowClick() {
        nextIndex = 0;
        $('#desc').slideUp('slow');
        // Depending on whether this is the next or previous
        // arrow, calculate the index of the next slide accordingly.
        if ($(this).hasClass('next')) {
            nextIndex = current >= slides.length - 1 ? 0 : current + 1;
        }
        else {
            nextIndex = current <= 0 ? slides.length - 1 : current - 1;
        }
        moveSlide(nextIndex);
    }
});
// This function takes an image and renders
// a version of it similar to the Overlay blending
// mode in Photoshop.
function createCanvasOverlay(image) {
    var canvas = document.createElement('canvas'),
            canvasContext = canvas.getContext("2d");    
    var img = new Image();
    img.src = '';
    img.onload = function () {
        // Make it the same size as the image
        canvas.width = slideshow.width;
        canvas.height = slideshow.height;

        var newWidth = 0, newHeight = 0;
        if (this.width > 0) {
            newWidth = (slideshow.height / this.height) * this.width;
            if (newWidth > slideshow.width) {
                newWidth = slideshow.width;
            }
        }
        if (this.height > 0) {
            newHeight = (slideshow.width / this.width) * this.height;
            if (newHeight > slideshow.height) {
                newHeight = slideshow.height;
            }
        }
        var xOffset = (slideshow.width - newWidth) / 2;
        var yOffset = (slideshow.height - newHeight) / 2;
        $(image).css('padding-left', xOffset + 'px');
        $(image).css('width', newWidth + 'px');
        $(image).css('padding-top', yOffset + 'px');
        $(image).css('height', newHeight + 'px');
        $(image).css('display', 'block');

        // Drawing the default version of the image on the canvas:
        canvasContext.drawImage(this, xOffset, yOffset, newWidth, newHeight);

        // Taking the image data and storing it in the imageData array:
        var imageData = canvasContext.getImageData(0, 0, canvas.width, canvas.height),
            data = imageData.data;

        // Loop through all the pixels in the imageData array, and modify
        // the red, green, and blue color values.
        for (var i = 0, z = data.length; i < z; i++) {

            // The values for red, green and blue are consecutive elements
            // in the imageData array. We modify the three of them at once:

            data[i] = ((data[i] < 128) ? (2 * data[i] * data[i] / 255) :
                        (255 - 2 * (255 - data[i]) * (255 - data[i]) / 255));
            data[++i] = ((data[i] < 128) ? (2 * data[i] * data[i] / 255) :
                        (255 - 2 * (255 - data[i]) * (255 - data[i]) / 255));
            data[++i] = ((data[i] < 128) ? (2 * data[i] * data[i] / 255) :
                        (255 - 2 * (255 - data[i]) * (255 - data[i]) / 255));

            // After the RGB channels comes the alpha value, which we leave the same.
            ++i;
        }
        // Putting the modified imageData back on the canvas.
        canvasContext.putImageData(imageData, 0, 0, 0, 0, imageData.width, imageData.height);
        // Inserting the canvas in the DOM, before the image:
        image.parentNode.insertBefore(canvas, image);
        if (++loaded == $('#slideshow img').length) {
            $('#slideshow .loading').hide();
        }
    }
    img.src = image.src;
}
function moveSlide(nextIndex) {
    var li = slides.eq(current),
                canvas = li.find('canvas')
    var next = slides.eq(nextIndex);

    if (supportCanvas) {
        // This browser supports canvas, fade it into view:
        canvas.fadeIn(function () {
            // Show the next slide below the current one:
            next.show();
            current = nextIndex;
            // Fade the current slide out of view:
            li.fadeOut(function () {
                li.removeClass('slideActive');
                canvas.hide();
                next.addClass('slideActive');
                $('#desc').html($('.slideActive img').attr('alt'));
            });
        });
    }
    else {
        // This browser does not support canvas.
        // Use the plain version of the slideshow.
        current = nextIndex;
        next.addClass('slideActive').show();
        li.removeClass('slideActive').hide();
        $('#desc').html($('.slideActive img').attr('alt'));
    }
    $('#desc').slideDown('slow');
}
function keypressed(e) {
    if (!hidden) {
        if (e.keyCode == 37 || e.keyCode == 39) {
            nextIndex = 0;
            $('#desc').slideUp('slow');

            if (e.keyCode == 39) {
                nextIndex = current >= slides.length - 1 ? 0 : current + 1;
            }
            else {
                nextIndex = current <= 0 ? slides.length - 1 : current - 1;
            }
            moveSlide(nextIndex);
        }
        if (e.keyCode == 38 || e.keyCode == 40) {
            y = $('div#thumbs').height() / 2;
            if (e.keyCode == 38) {
                startIndex -= 0.25;
                if (startIndex < 0) {
                    startIndex = images.length - 1;
                }
            } else {
                startIndex += 0.25;
                if (startIndex >= images.length) {
                    startIndex = 0;
                }
            }
            if (y > 0 && y < canvasLength) {
                redraw();
            }
        }
    }
}
function thumbSelected(y) {
    var index = ((y - 5) / 121.0) - startIndex;
    if (index < 0) {
        index += images.length;
    }
    if (Math.floor(index) != current) {
        moveSlide(Math.floor(index));
    }
}
function thumbDown(e) {
    if (!hidden) {
        var canvas = $('#thumbs canvas')[0];
        var y;
        if (e.pageY) {
            y = e.pageY;
        }
        else {
            y = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;
        }
        y -= canvas.offsetTop;

        thumbSelected(y);
    }
}
function redraw() {
    var thumbs = { width: 120, height: 120 };
    thumbs.height = $('div#thumbs').height();
    var canvas = $('#thumbs canvas')[0],
        canvasContext = canvas.getContext("2d");
    canvas.width = thumbs.width;
    canvas.height = thumbs.height;
    canvasContext.clearRect(0, 0, canvas.width, canvas.height);
    $.each(images, function (index, img) {
        var newWidth = 0, newHeight = 0;
        if (img.width > 0) {
            newWidth = (thumbs.height / img.height) * img.width;
            if (newWidth > thumbs.width) {
                newWidth = thumbs.width;
            }
        }
        if (img.height > 0) {
            newHeight = (thumbs.width / img.width) * img.height;
            if (newHeight > thumbs.height) {
                newHeight = thumbs.height;
            }
        }
        var xOffset = (thumbs.width - newWidth) / 2;
        var yOffset = (120 - newHeight) / 2;
        if (startIndex + index >= images.length) {
            canvasContext.drawImage(img, xOffset, 5 + 121 * (index + startIndex - images.length) + yOffset, newWidth, newHeight);            
        } else {
            canvasContext.drawImage(img, xOffset, 5 + 121 * (index + startIndex) + yOffset, newWidth, newHeight);
        }
    });
}

function drawThumbnails() {
    var thumbs = { width: 120, height: 120 },
        thumbIndex = 0;
    thumbs.height = $('div#thumbs').height();
    var canvas = $('#thumbs canvas')[0],
        canvasContext = canvas.getContext("2d");
    // Make it the same size as the image
    canvas.width = thumbs.width;
    canvas.height = thumbs.height;
    y = canvas.height / 2;

    $('#slideshow img').each(function (index) {
        var img = new Image();
        img.onload = function () {
            var newWidth = 0, newHeight = 0;
            if (this.width > 0) {
                newWidth = (thumbs.height / this.height) * this.width;
                if (newWidth > thumbs.width) {
                    newWidth = thumbs.width;
                }
            }
            if (this.height > 0) {
                newHeight = (thumbs.width / this.width) * this.height;
                if (newHeight > thumbs.height) {
                    newHeight = thumbs.height;
                }
            }
            var xOffset = (thumbs.width - newWidth) / 2;
            var yOffset = (120 - newHeight) / 2;
            canvasContext.drawImage(this, xOffset, 5 + 121 * index + yOffset, newWidth, newHeight);
            
            canvasLength = 5 + 121 * thumbIndex++ + 120;
            images[index] = this;
        };
        img.src = this.src;
    });
}
function resize(e) {
    slideshow.width = $('.slides').width(); //820;
    slideshow.height = $('.slides').height(); // 620;
//    if (supportCanvas) {
//        $('#slideshow img').each(function () {
//            // Rendering the modified versions of the images:
//            createCanvasOverlay(this);
//        });
//    }
    redraw();
}