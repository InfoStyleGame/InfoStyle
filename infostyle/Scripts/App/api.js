window.Api = {
    root: "http://norris.kontur:4444/api",
    GetTask: function (type, subject) {
        return $.get(Api.root + "/task", { type: type, subject: subject })
            .fail(function(data) { alert("Error"); });
    },

    SubmitTaskResults: function(score, taskId) {
        return $.post(Api.root + "/taskResult?score=" + score + "&taskId=" + taskId);
    }
};

window.mockDef = function(result) {
    var $def = $.Deferred();
    $def.resolve(result);
    return $def.promise();
};

window.TaskTypes = {
    CrawlLine: "CrawlLine"
};

window.Subjects = {
    Stop: "Stop"
};

window.TextTypes = {
    Normal: "Normal",
    Stop : "Stop"
}