/// <reference path="jquery.d.ts" />
function addPenetration() {
    document.getElementById('addPenetration').style.display = 'none';
    document.getElementById('removePenetration').style.display = 'block';
    document.getElementById('penetration-test').style.display = 'block';
    (document.getElementById('TestType') as any).value = 'RewetAndPenetrationTime';
}
function removePenetration() {
    document.getElementById('addPenetration').style.display = 'block';
    document.getElementById('removePenetration').style.display = 'none';
    document.getElementById('penetration-test').style.display = 'none';
    (document.getElementById('TestType') as any).value = 'Rewet';
}
$(document).ready(() => {
    $('#addPenetration').click(() => {
        addPenetration();
    });
    $('#removePenetration').click(() => {
        removePenetration();
    });
});