//Autocomplete for inputs

$(function () {

    var submitAutocompleteForm = function (event, ui) {

        var input = $(this);

        input.val(ui.item.label);

        var form = input.parents("form:first");

        form.submit();
    };

    var assignAutoCompleteAction = function () {

        var input = $(this);

        var action = {
            source: input.attr("data-autocomplete-action"),
            select: submitAutocompleteForm
        };

        //Jquery UI autocomplete method with custom render item
        input.autocomplete(action)
            .data("uiAutocomplete")._renderItem = function (ul, item) {
            return $("<li />")
                .data("item.autocomplete", item)
                .append('<div class="profile-thumbnail" style="background-image: url(' + item.profilePicUrl + ');display:inline-block;"/>' +
                '<a class="searchResults" href="/' + item.username + '">' + item.label + '</a>')
                .appendTo(ul);
            }; ;
    };

    $("input[data-autocomplete-action]").each(assignAutoCompleteAction);

    

});