$(window).load(function () {
    var actualHeight = $(document).height();
    Platform.Canvas.setHeight(actualHeight);
});

$(function() {
    $('.ask').click(function () {
        var productId = $(this).parent().data("product-id");
        var token = getParameterByName("token");
        $('.question-dialog').dialog({
            buttons: {
                "OK": function () {
                    if ($('.question').val() == '') {
                        $('.question').focus();
                    } else {
                        var question = $(".question").val();
                        $.ajax({
                            url: '/question/ask',
                            data: { question: question, productId: productId, token: token },
                            success: function () {
                                $('.question').clear();
                                
                            },
                            error: function () {
                                $('.question').clear();
                                $('.question-error-dialog').dialog();
                            }
                        });
                        $(this).dialog('close');
                    }
                }
            }
        });
    });
});

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.search);
    if (results == null)
        return "";
    else
        return decodeURIComponent(results[1].replace(/\+/g, " "));
}