$(document).ready(function(){
    $("#debug-randominsert").click(function () {
        $.post("/Shared/AjaxHost.cshtml", {
            type: 'randomInsert'
        },function(){
            $("#alert").text("随机插了一下");
        });
    });
});
