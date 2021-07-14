


$(document).ready(function () {
    $('#uploadForm').submit(function (e) {

        
        e.preventDefault();
        var articleName = $("input[name=articleName]").val();
        var articleAuthor = $("input[name=articleAuthor]").val();
        var catergorie = $("#catergorie").val();
        var articleContent = $('<div>').append($('#textarea').clone()).html();
       
        
        $.ajax({
            type: 'POST',
            url: "/Article/AccessUpload",
            data: {
                ArticleName: articleName,
                ArticleAuthor: articleAuthor,
                Categorie: catergorie,
                ArticleContent: htmlEncode(articleContent),
            },
            dataType: "json",
            success: function (response) {
                if (response.check == true) {
                    $("#demo").text(response.messageSucceed);
                } else {
                    $("#demo").text(response.messageFail);
                }
            }
        })

    });
});

function htmlEncode(value) {
    // Create a in-memory element, set its inner text (which is automatically encoded)
    // Then grab the encoded contents back out. The element never exists on the DOM.
    return $('<textarea/>').text(value).html();
}

function htmlDecode(value) {
    return $('<textarea/>').html(value).text();
}






