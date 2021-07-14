




function htmlDecode(value) {
    return $('<textarea/>').html(value).text();
}
document.getElementById("textarea").contentEditable = false;