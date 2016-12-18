/// <reference path="jquery.d.ts" />
declare var globalConfig: any;

function sendForm(id: any, name: any, visibleStatus: any);
function sendForm(id, name, visibleStatus) {
    $.ajax({
        url: globalConfig.updateSettingsUrl,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id,
            Name: name,
            Visible: visibleStatus
        }),
        success() {
            location.reload();
        },
        error() {
            alert('Interner Server Error - Sichtbarkeitsänderung wurde nicht ausgeführt.');
        }
    });
}

$(() => {
        $('.visible-toggle')
            .change(function () {
                const $this : JQuery = $(this);
                console.log('Toggle: ' + $this.prop('checked'));
                sendForm($this.prop('id'), $this.prop('name'), $this.prop('checked'));
            });
    }
);