





var src;
function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        reader.onload = function (e) {
            $('#blah')
                .attr('src', e.target.result);
            src = e.target.result
        };

        reader.readAsDataURL(input.files[0]);
    }
}


$(document).ready(function () {
    $('#avatar').submit(function (e) {

        e.preventDefault();
        var username = $("input[name=username]").val();
        var email = $("input[name=email]").val();
        var link = $("input[name=link]").val();
        var name = $("input[name=name]").val();
        var password = $("input[name=password]").val();
        var confirmPassword = $("input[name=confirmPassword]").val();

        var $detailDiv = $('#data');
        $.ajax({
            type: 'POST',
            url: "/Home/AccessRegister",
            data: {
                username: username,
                email: email,
                link: link,
                name: name,
                password: password,
                confirmPassword: confirmPassword,
                code: src,

            },
            dataType: "json",
            success: function (response) {
                if (response.check == true) {
                    $.get("/Home/DemoUp", function (data) {
                        contentType: 'application/json; charset=utf-8',
                            $detailDiv.replaceWith(data);
                    });
                    window.location.replace("/Home/Login")
                } else {
                    $("#message").text("Cant let input empty") ;
                }
            }              
            })
        })
    })

