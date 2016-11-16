/// <reference path="jquery.d.ts" />
function addNote() {
    var path = document.getElementById('addNote').getAttribute('value');
    document.getElementById('submitForm').action = path;
    document.getElementById('submitForm').submit();
}
$(document).ready(function () {
    $('#addNote').click(function () {
        if ($('#submitForm').valid()) {
            addNote();
        }
    });
    $('#submitForm').submit(function () {
        if ($(this).valid()) {
            $(this).find(':submit').attr('disabled', 'disabled');
        }
    });
});
