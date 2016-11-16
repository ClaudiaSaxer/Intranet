/// <reference path="jquery.d.ts" />
function sendForm(id: any, name: any, visibleStatus: any);

function sendForm(id, name, visibleStatus) {
    $.ajax({
        url: '/Settings/Update',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id,
            Name: name,
            Visible: visibleStatus
        }),
        error() {
            alert('Interner Server Error - Sichtbarkeitsänderung wurde nicht ausgeführt.');
        }
    });
}

$(() => {
        $('.visible-toggle')
            .change(function () {
                console.log('Toggle: ' + $(this).prop('checked'));
                sendForm($(this).prop('id'), $(this).prop('name'), $(this).prop('checked'));
            });
    }
);