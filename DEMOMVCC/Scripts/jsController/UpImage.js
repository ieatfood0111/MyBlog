





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
                    $("#message").text("Cant let input empty");
                }
            }
        })
    })
})




$(document).ready(function () {
    $('#avatarChange').click(function (e) {
        $('#background').append(`
    <div id="modal" class="modal" tabindex="-1" role="dialog">
            <div class="modal-dialog" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <h5 class="modal-title">Change Avatar</h5>
                    </div>
                    <div class="modal-body">
                        <div>

                        <form id="updateAvatar">
                            <label for="img">Select image:</label>
                            <input  type="file" id="img" name="img" accept="image/*" onchange="readURL(this);">
                        </form>
                        </div>
                        <img style="width:200px;height:200px" id="blah">
<div id="message"></div>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" id="SaveBtn">Save changes</button>
                        <button type="button" class="btn btn-secondary" id="closeBtn" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
    <script>

    </script>

`);


        $('#modal').css('display', 'block');
        $('#closeBtn').click(function (e) {
            $('#modal').css('display', 'none');
            $('#background').empty();
        })
        $('#SaveBtn').click(function () {

            $('#updateAvatar').submit(function (e) {
                e.preventDefault();
                console.log("alo")
                $.ajax({
                    type: 'POST',
                    url: "/User/ChangeAvatar",
                    data: {
                        code: src,
                    },
                    dataType: "json",
                    success: function (response) {
                        if (response.check == true) {
                            $('#avatarChange').attr('src', response.code);
                            $("#message").text("Change avatar successful");
                        } else {
                            $("#message").text("Cant change avatar");
                        }
                    }
                })
            })
            $('#updateAvatar').submit()
        })
    })

});

