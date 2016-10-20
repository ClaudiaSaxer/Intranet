function sendForm(id,name,visibleStatus) {
    $.ajax({
        url: '/Settings/Update',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify({
            Id: id,
            Name: name,
            Visible: visibleStatus
        }),
        error: function () {
            alert('Interner Server Error - Sichtbarkeitsänderung wurde nicht ausgeführt.');
        }
    });
}

$(function () {
    $('.visible-toggle')
        .change(function () {
            console.log('Toggle: ' + $(this).prop('checked'));
            sendForm($(this).prop('id'), $(this).prop('name'), $(this).prop('checked'));
        });
});
