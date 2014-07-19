$(document).ready(function () {
    var alittlelist = [];
    for (var i = 2; i < 40; i++) { alittlelist.push(i); }
    for (var i in alittlelist) {
        try {
            document.getElementById('c' + i).onblur = new Function("{$.post('/Shared/AjaxHost.cshtml', {from: window.location.pathname, type: 'check', name: 'c' + " + i + ", value: document.getElementById('c' + " + i + ").value }, function (data, status) { $('#checktipc' + " + i + ").text(data)});}");
            console.log();
        } catch (err) { continue; }
    }
});