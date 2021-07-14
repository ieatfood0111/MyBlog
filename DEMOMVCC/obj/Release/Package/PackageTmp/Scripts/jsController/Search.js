

var keyword;
$(document).ready(function () {
    $('#searchForm').submit(function (e) {
        e.preventDefault();
        e.stopPropagation();

        var $detailDiv = $('#data');

        var keyword = $("#searchInput").val();
        var url = $(this).data('url');
        $.get(url + keyword, function (data) {
            contentType: 'application/json; charset=utf-8',
                $detailDiv.replaceWith(data);
        });
    });

});


function page(page, keyword) {
    var $detailDiv = $('#data');
        var url = "/Search/Index/" + page + "?keyword=" + keyword;
        $.get(url, function (data) {
            contentType: 'application/json; charset=utf-8',
                $detailDiv.replaceWith(data);
        
    })
}


function demo(page,keyword) {
    alert(page + "|" + keyword)
}
$(document).ready(function () {
    $('#searchInput').on('input', function () {
        $(".instant-results").fadeIn('slow').css('height', 'auto');
        $(".instant-results").css('position', 'absolute');
        var $detailDiv = $('#listArc');
        var keyword = $("#searchInput").val();
        var url = $(this).data('url');   
        $.get(url + keyword, function (data) {
            contentType: 'application/json; charset=utf-8',
            $detailDiv.replaceWith(data);
        });

    });

    $(document).ready(function () {
        //Open Search    
        //$('#searchInput').on('input',function (event) {
        //    $(".instant-results").fadeIn('slow').css('height', 'auto');
        //    
        //});

        $('#searchInput').focusout(function (event) {
            $(".instant-results").fadeOut('500');
        });
    });
});
