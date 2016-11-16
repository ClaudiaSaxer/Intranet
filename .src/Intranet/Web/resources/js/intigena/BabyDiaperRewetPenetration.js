/// <reference path="jquery.d.ts" />
function addPenetration() {
    document.getElementById('addPenetration').style.display = 'none';
    document.getElementById('removePenetration').style.display = 'block';
    document.getElementById('penetration-test').style.display = 'block';
    document.getElementById('TestType').value = 'RewetAndPenetrationTime';
}
function removePenetration() {
    document.getElementById('addPenetration').style.display = 'block';
    document.getElementById('removePenetration').style.display = 'none';
    document.getElementById('penetration-test').style.display = 'none';
    document.getElementById('TestType').value = 'Rewet';
}
$(document).ready(function () {
    $('#addPenetration').click(function () {
        addPenetration();
    });
    $('#removePenetration').click(function () {
        removePenetration();
    });
});
