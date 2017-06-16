//Ajax form submission

$(function() {

    var ajaxFormSubmit = function () {

        var form = $(this);

        var settings = {
            action: form.attr("action"),
            method: form.attr("method"),
            data: form.serialize()
        };

        $.ajax(settings).done(function (responseData) {

            var target = $(form.attr("data-update-target"));

            target.replaceWith(responseData);

        });

        return false;
    };

    $("form[data-ajax-submit = 'true']").submit(ajaxFormSubmit);
});
