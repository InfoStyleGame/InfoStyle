window.Api = {
    root: "/api",
    GetTask: function (type) {
        return $.get(Api.root + "/task", { type: type })
            .fail(function(data) { alert("Error"); });
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

window.TextTypes = {
    Normal: "Normal",
    Stop : "Stop"
}