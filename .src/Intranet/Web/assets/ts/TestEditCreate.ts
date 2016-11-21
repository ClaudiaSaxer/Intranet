/// <reference path="jquery.d.ts" />

function addNote() {
    const path = document.getElementById('addNote').getAttribute('value');
    (document.getElementById('submitForm') as any).action = path;
    (document.getElementById('submitForm') as any).submit();
}
$(document).ready(() => {
    $('#addNote').click(() => {
        if (($('#submitForm') as any).valid()) {
            addNote();
        }
    });
    $('#submitForm').submit(function () {
        if (($(this) as any).valid()) {
            $(this).find(':submit').attr('disabled', 'disabled');
        }
    });
});