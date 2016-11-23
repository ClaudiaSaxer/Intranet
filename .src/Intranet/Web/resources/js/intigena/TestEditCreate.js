/// <reference path="jquery.d.ts" />
function addNote() {
    var path = document.getElementById('addNote').getAttribute('value');
    document.getElementById('submitForm').action = path;
    document.getElementById('submitForm').submit();
}
function deleteNote(id) {
    document.getElementById('Note-' + id).style.display = 'none';
    $('#Notes_' + id + '__ErrorCodeId').val(-1);
    console.log($('#Notes_' + id + '__ErrorCodeId').attr('value'));
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
    $('.addNote').click(function () {
        deleteNote(this.id);
    });
});
